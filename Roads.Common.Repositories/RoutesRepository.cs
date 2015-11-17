using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Roads.Common.Integrations;
using Roads.Common.Models.DataContract;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Repositories {
	/// <summary>
	/// The Routes Repository.
	/// </summary>
	public class RoutesRepository : RepositoryBase {
		#region Private constants

		private const int DefaultSearchDepth = 5;

		private const int DefaultNumberRecordsPerPage = 10;

		#endregion

		#region Constructors and Destructors

		/// <summary>Initializes a new instance of the <see cref="RoutesRepository"/> class.</summary>
		/// <param name="dataContext">The data context.</param>
		public RoutesRepository( IDataContext dataContext )
			: base( dataContext ) {
		}

		/// <summary>Initializes a new instance of the <see cref="RoutesRepository"/> class.</summary>
		public RoutesRepository()
			: base( new DataContext() ) {
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the children's nodes ids.
		/// </summary>
		/// <param name="cityNodeId">The city node identifier.</param>
		/// <returns>The List of <see cref="int"/>.</returns>
		public List<int> GetChildrensNodesIds( int cityNodeId ) {
			return
				DataContext.RouteNodes.Where( w => w.OriginCityNodeId == cityNodeId )
							.Select( s => s.DestinationCityNodeId ).ToList();
		}

		/// <summary>
		/// Gets the parents nodes ids.
		/// </summary>
		/// <param name="cityNodeId">The city node identifier.</param>
		/// <returns>The List of <see cref="int"/>.</returns>
		public List<int> GetParentsNodesIds( int cityNodeId ) {
			return
				DataContext.RouteNodes.Where( w => w.DestinationCityNodeId == cityNodeId )
							.Select( s => s.OriginCityNodeId ).ToList();
		}

		/// <summary>
		/// Gets all relations ids.
		/// </summary>
		/// <param name="cityNodeId">The city node identifier.</param>
		/// <returns>The List of <see cref="int"/>.</returns>
		public List<int> GetAllRelationsIds( int cityNodeId ) {
			List<int> childs = GetChildrensNodesIds( cityNodeId );

			childs.AddRange( GetParentsNodesIds( cityNodeId ) );

			return childs.Distinct().ToList();
		}

		/// <summary>
		/// Gets the route node.
		/// </summary>
		/// <param name="originCityNodeId">The origin city node identifier.</param>
		/// <param name="destinationCityNodeId">The destination city node identifier.</param>
		/// <returns>The <see cref="RouteNode"/> node or null.</returns>
		public RouteNode GetRouteNode( long originCityNodeId, long destinationCityNodeId ) {
			var routeNode =
				DataContext.RouteNodes.FirstOrDefault(
					f => f.OriginCityNodeId == originCityNodeId && f.DestinationCityNodeId == destinationCityNodeId );

			if( routeNode != null ) {
				routeNode.DestinationCityNode =
					DataContext.CityNodes.SingleOrDefault( m => m.CityNodeId == routeNode.DestinationCityNodeId );

				routeNode.OriginCityNode =
					DataContext.CityNodes.SingleOrDefault( m => m.CityNodeId == routeNode.OriginCityNodeId );
			}

			return routeNode;
		}

		/// <summary>
		/// Gets the route details feedbacks.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="language"></param>
		/// <returns> The List of <see cref="RouteDetailsFeedback"/>.</returns>
		public List<RouteDetailsFeedback> GetRoutDetailsFeedbackFor( RouteNode node, string language ) {
			if( node == null ) {
				throw new ArgumentNullException( "node" );
			}

			if( String.IsNullOrEmpty( language ) ) {
				throw new ArgumentNullException( "language" );
			}

			var result = new List<RouteDetailsFeedback>();

			var languageObject = DataContext.Languages.FirstOrDefault( k => k.Name.ToLower() == language.ToLower() );

			int languageId = DataContext.Languages.FirstOrDefault( k => k.IsDefault == true ).LanguageId;

			if( languageObject != null ) {
				languageId = languageObject.LanguageId;
			}

			var feedbacks = DataContext.Feedbacks.Where( e => e.RouteNodeId == node.RouteNodeId ).ToList();

			var destinationCityName = DataContext.MapObjectTranslations.SingleOrDefault(
				m => m.LanguageKey == node.DestinationCityNode.LanguageKey && m.LanguageId == languageId );

			var originCityName = DataContext.MapObjectTranslations.SingleOrDefault(
				m => m.LanguageKey == node.OriginCityNode.LanguageKey && m.LanguageId == languageId );

			foreach( var feedback in feedbacks ) {
				if( feedback != null ) {
					var feedbackValues = DataContext.FeedbackValues.Where( m => m.FeedbackId == feedback.FeedbackId );
					var feedbackValuesData = ( from value in feedbackValues
											   let feedbackItem = DataContext.FeedbackItems.FirstOrDefault( f => f.FeedbackItemId == value.FeedbackItemId )
											   where feedbackItem != null
											   select new FeedbackValueData {
												   FeedbackId = value.FeedbackId,
												   FeedbackItem = new FeedbackItemData {
													   DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey,
													   NameTranslationKey = feedbackItem.NameTranslationKey,
													   SortNumber = feedbackItem.SortNumber,
													   FeedbackModelId = feedbackItem.FeedbackModelId,
													   FeedbackItemId = feedbackItem.FeedbackItemId,
													   IsNumeric = feedbackItem.IsNumeric,
													   Mandatory = feedbackItem.Mandatory
												   },
												   FeedbackItemId = value.FeedbackItemId,
												   FeedbackValueId = value.FeedbackValueId,
												   Value = value.Value
											   } ).ToList();

					feedback.User = DataContext.Users.SingleOrDefault( m => m.UserId == feedback.UserId );

					if( feedback.User != null ) {
						var holisticFeedback = new RouteDetailsFeedback {
							UserName = feedback.User.Name,
							FeedbackId = feedback.FeedbackId,
							SubmitTime = feedback.SubmitTime,
							OriginCityName = originCityName != null ? originCityName.Value : String.Empty,
							DestinationCityName = destinationCityName != null ? destinationCityName.Value : String.Empty,
							DestinationCityId = feedback.RouteNode.DestinationCityNodeId,
							OriginCityId = feedback.RouteNode.OriginCityNodeId,
							FeedbackValues = feedbackValuesData
						};
						result.Add( holisticFeedback );
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Gets the holistic feedback for.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="language"></param>
		/// <returns> The List of <see cref="HolisticFeedback"/>.</returns>
		public List<HolisticFeedback> GetHolisticFeedbackFor( RouteNode node, string language ) {
			var result = new List<HolisticFeedback>();

			var languageObject = DataContext.Languages.FirstOrDefault( k => k.Name.ToLower() == language.ToLower() );

			int languageId = DataContext.Languages.FirstOrDefault( k => k.IsDefault == true ).LanguageId;

			if( languageObject != null ) {
				languageId = languageObject.LanguageId;
			}

			var feedbacks = DataContext.Feedbacks.Where( e => e.RouteNodeId == node.RouteNodeId ).ToList();

			var destinationCityName = DataContext.MapObjectTranslations.SingleOrDefault(
				m => m.LanguageKey == node.DestinationCityNode.LanguageKey && m.LanguageId == languageId );

			var originCityName = DataContext.MapObjectTranslations.SingleOrDefault(
				m => m.LanguageKey == node.OriginCityNode.LanguageKey && m.LanguageId == languageId );

			foreach( var feedback in feedbacks ) {
				feedback.User = DataContext.Users.SingleOrDefault( m => m.UserId == feedback.UserId );
				var holisticFeedback = new HolisticFeedback {
					UserId = feedback.UserId,
					UserName = feedback.User.Name,
					FeedbackId = feedback.FeedbackId,
					SubmitTime = feedback.SubmitTime,
					OriginCityName = originCityName != null ? originCityName.Value : String.Empty,
					DestinationCityName = destinationCityName != null ? destinationCityName.Value : String.Empty
				};

				var feedbackValue = DataContext.FeedbackValues.FirstOrDefault( f => f.FeedbackId == feedback.FeedbackId );

				if( feedbackValue != null ) {
					holisticFeedback.Value = feedbackValue.Value;

					var feedbackItem =
						DataContext.FeedbackItems.FirstOrDefault( f => f.FeedbackItemId == feedbackValue.FeedbackItemId );
					if( feedbackItem != null ) {

						holisticFeedback.FeedbackItem = new FeedbackItemData {
							DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey,
							FeedbackItemId = feedbackItem.FeedbackItemId,
							FeedbackModelId = feedbackItem.FeedbackModelId,
							IsNumeric = feedbackItem.IsNumeric,
							NameTranslationKey = feedbackItem.NameTranslationKey,
							Mandatory = feedbackItem.Mandatory,
							SortNumber = feedbackItem.SortNumber
						};
					}
				}

				result.Add( holisticFeedback );
			}

			return result;
		}

		/// <summary>
		/// Gets the searching depth.
		/// </summary>
		/// <returns> The Searching Depth value from database or default value.</returns>
		public int GetSearchingDepth() {
			int value = DefaultSearchDepth;

			Setting setting = DataContext.Settings.FirstOrDefault( e => e.SettingName == "SearchDepth" );

			if( setting != null ) {
				if( !int.TryParse( setting.SettingValue, out value ) ) {
					value = DefaultSearchDepth;
				}
			}

			return value;
		}

		/// <summary>
		/// Gets the NumberRecordsPerPage.
		/// </summary>
		/// <returns> The NumberRecordsPerPage value from database or default value.</returns>
		public int GetNumberOfRecordsPerPage() {
			int value = DefaultNumberRecordsPerPage;

			Setting setting = DataContext.Settings.FirstOrDefault( e => e.SettingName == "NumberOfRecordPerPage" );

			if( setting != null ) {
				if( !int.TryParse( setting.SettingValue, out value ) ) {
					value = DefaultSearchDepth;
				}
			}

			return value;
		}

		/// <summary>
		/// Adds the trek if not exist.
		/// </summary>
		/// <param name="trek">The trek.</param>
		public void AddTrekIfNotExist( Trek trek ) {
			if( !DataContext.Treks.Any( e => e.Track == trek.Track ) ) {
				DataContext.AddDataObject( trek );

				Save();
			}
		}

		/// <summary>
		/// GetLanguageById the treks.
		/// </summary>
		/// <param name="startedCityId">The started city identifier.</param>
		/// <param name="destinationCityId">The destination city identifier.</param>
		/// <param name="page">The page.</param>
		/// <param name="languageName">Name of the language.</param>
		/// <returns>
		/// The list of <see cref="Trek" />.
		/// </returns>
		public async Task<RoutesSearchResultData> GetRoutesSearchResultData( int startedCityId, int destinationCityId, int page, string languageName, bool isRouteValidation ) {
			var recordsPerPage = GetNumberOfRecordsPerPage();
			List<Trek> tracksWithoutFilter = new List<Trek>();

			tracksWithoutFilter = DataContext
			.Treks.Where(
				w => w.OriginCityNodeId == startedCityId
				&& w.DesinationCityNodeId == destinationCityId
			)
			.ToList();

			if( isRouteValidation ) {
				List<long> tracksToRemove = new List<long>();
				for( int i = 0; i < tracksWithoutFilter.Count;i++ ) {
					bool isDistanceValid = await ValidateTrack( tracksWithoutFilter[i].Track );
					if( !isDistanceValid ) {
						tracksToRemove.Add( tracksWithoutFilter[i].TrekId );
					}
				}
				tracksWithoutFilter.RemoveAll(track=>tracksToRemove.Contains(track.TrekId));
			}

			int count = tracksWithoutFilter.Count();

			if( startedCityId == -1 && destinationCityId == -1 ) {
				count = DataContext.Treks.Count();
			}

			if( count != 0 ) {
				var language = DataContext.Languages.FirstOrDefault( e => e.Name == languageName ) ??
							   DataContext.Languages.FirstOrDefault( e => e.IsDefault );

				var ckipValue = ( recordsPerPage * page ) >= count ? ( recordsPerPage * ( page - 1 ) ) : recordsPerPage * page;

				var serchDepth = DataContext.Settings.FirstOrDefault( e => e.SettingName == "SearchDepth" );

				byte currentSerchDepth = DefaultSearchDepth;

				if( serchDepth != null ) {
					byte.TryParse( serchDepth.SettingValue, out currentSerchDepth );
				}


				var traks = tracksWithoutFilter
					.Where( e => e.NodesCount <= currentSerchDepth )
					.OrderByDescending( e => e.Track.Length )
					.Skip( ckipValue )
					.Take( recordsPerPage )
					.Select( s => new TrekData {
						DesinationCityNodeId = s.DesinationCityNodeId,
						OriginCityNodeId = s.OriginCityNodeId,
						Hash = s.Hash,
						TrekId = s.TrekId,
						Track = s.Track,
						TreckDate = s.TrekDate
					} )
					.ToList();

				if( startedCityId == -1 && destinationCityId == -1 ) {
					traks = DataContext
					.Treks
					.Where( e => e.NodesCount <= currentSerchDepth )
					.OrderByDescending( e => e.TrekId )
					.Skip( ckipValue )
					.Take( recordsPerPage )
					.Select( s => new TrekData {
						DesinationCityNodeId = s.DesinationCityNodeId,
						OriginCityNodeId = s.OriginCityNodeId,
						Hash = s.Hash,
						TrekId = s.TrekId,
						Track = s.Track,
						TreckDate = s.TrekDate
					} )
					.ToList();
				}

				foreach( var track in traks ) {
					try {
						track.OriginCityNodeName = GetCityNodeName( track.Track, true, language.LanguageId );
						track.DesinationCityNodeName = GetCityNodeName( track.Track, false, language.LanguageId );
						track.FeedbackCount = GetTrackFeedbacksCount( track.Track );
						track.TreckDate = new DateTime();
					} catch( Exception exception ) {
						track.Hash = "ffff";
						//Ignore
					}

				}

				return new RoutesSearchResultData {
					Count = count,
					ActualRange = ckipValue + recordsPerPage,
					PageNumber = page,
					RecordsPerPage = recordsPerPage,
					Treks = traks.OrderByDescending( e => e.TreckDate ).ToList()
				};
			}

			return new RoutesSearchResultData();

		}

		/// <summary>
		/// Gets the feedback controls for new feedback.
		/// </summary>
		/// <returns>GetLanguageById all feedback models and items in DB</returns>
		public IEnumerable<FeedbackItem> GetFeedbackControlsForNewFeedback() {
			IEnumerable<FeedbackItem> feedbackItems = DataContext.FeedbackItems.Include( x => x.FeedbackModel ).ToList();
			return feedbackItems;
		}

		/// <summary>
		/// Get the trek by hash.
		/// </summary>
		/// <param name="hash">The hash.</param>
		/// <returns>Return the trek.</returns>
		public Trek GetTrekByHash( string hash ) {
			return DataContext.Treks.SingleOrDefault( m => m.Hash == hash );
		}

		/// <summary>
		/// Gets the feedback model by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Return feedback model item.</returns>
		public FeedbackModel GetFeedbackModelById( long id ) {
			return DataContext.FeedbackModels.SingleOrDefault( m => m.FeedbackModelId == id );
		}

		/// <summary>
		/// Creates the route node.
		/// </summary>
		/// <param name="originCityId">The origin city identifier.</param>
		/// <param name="destinationCityId">The destination city identifier.</param>
		/// <returns>Return id of added routeNode</returns>
		public int CreateRouteNode( int originCityId, int destinationCityId ) {
			var routeNode = new RouteNode {
				OriginCityNodeId = originCityId,
				DestinationCityNodeId = destinationCityId
			};
			DataContext.AddDataObject( routeNode );
			Save();
			return routeNode.RouteNodeId;
		}


		/// <summary>
		/// Gets the hash by track.
		/// </summary>
		/// <param name="track">The track.</param>
		/// <returns>Return hash by track</returns>
		public string GetHashByTrack( string track ) {
			return DataContext.Treks.FirstOrDefault( x => x.Track == track ).Hash;
		}

		/// <summary>
		/// Parses the track.
		/// </summary>
		/// <param name="track">The track.</param>
		/// <returns>The list of <see cref="RouteNode"/>.</returns>
		public List<RouteNode> ParseTreck( string track ) {
			string[] cityPoints = track.Split( '-' );

			var nodes = new List<RouteNode>();

			for( int i = 0; i < ( cityPoints.Length - 1 ); i++ ) {
				var original = GetPointId( true, int.Parse( cityPoints[i] ), int.Parse( cityPoints[i + 1] ) );

				var destination = GetPointId( false, int.Parse( cityPoints[i] ), int.Parse( cityPoints[i + 1] ) );

				var routrnode = DataContext.RouteNodes.FirstOrDefault( e => e.OriginCityNodeId == original && e.DestinationCityNodeId == destination ) ??
								DataContext.RouteNodes.FirstOrDefault( e => e.OriginCityNodeId == destination && e.DestinationCityNodeId == original );

				if( routrnode != null ) {
					nodes.Add( routrnode );
				}
			}

			return nodes;
		}

		/// <summary>
		/// Parses the track.
		/// </summary>
		/// <param name="track">The track.</param>
		/// <returns>The list of <see cref="CityNode"/>.</returns>
		public List<CityNode> GetTreckCities( string track ) {
			string[] cityPoints = track.Split( '-' );

			var cities = new List<CityNode>();

			foreach( var point in cityPoints ) {
				long id = 0;

				if( long.TryParse( point, out id ) ) {
					var city = DataContext.CityNodes.FirstOrDefault( e => e.CityNodeId == id );

					if( city != null ) {
						cities.Add( city );
					}
				}

			}

			return cities;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Gets the point identifier.
		/// </summary>
		/// <param name="isBegin">if set to <c>true</c> [is begin].</param>
		/// <param name="pointId1">The point id1.</param>
		/// <param name="pointId2">The point id2.</param>
		/// <returns>Point Id.</returns>
		private int GetPointId( bool isBegin, int pointId1, int pointId2 ) {
			if( isBegin ) {
				return pointId1 < pointId2
					? pointId1
					: pointId2;
			}

			return pointId1 > pointId2
				? pointId1
				: pointId2;
		}

		/// <summary>
		/// Gets the track feedbacks count.
		/// </summary>
		/// <param name="trek">The trek.</param>
		/// <returns>The track Feedbacks count.</returns>
		private long GetTrackFeedbacksCount( string trek ) {
			var count = 0;

			foreach( var routeNode in ParseTreck( trek ) ) {
				count += DataContext.Feedbacks.Count( e => e.RouteNodeId == routeNode.RouteNodeId );
			}

			return count;
		}

		/// <summary>
		/// Gets the name of the origin city node.
		/// </summary>
		/// <param name="trek">The trek.</param>
		/// <param name="getOrigin">if set to <c>true</c> [get origin].</param>
		/// <param name="languageId">The language identifier.</param>
		/// <returns>
		/// Origin City Node Name
		/// </returns>
		private string GetCityNodeName( string trek, bool getOrigin, int languageId ) {
			var nodes = GetTreckCities( trek );

			if( !nodes.Any() ) {
				return string.Empty;
			}

			var city = getOrigin ? nodes.First() : nodes.Last();

			var result = string.Empty;

			var cityName =
				DataContext.MapObjectTranslations.FirstOrDefault(
					e => e.LanguageKey == city.LanguageKey && e.LanguageId == languageId );

			if( cityName != null ) {
				result = cityName.Value;
			}

			var region = DataContext.RegionNodes.FirstOrDefault( e => e.RegionNodeId == city.RegionNodeId );

			if( region != null ) {
				var regionName =
				DataContext.MapObjectTranslations.FirstOrDefault(
					e => e.LanguageKey == region.LanguageKey && e.LanguageId == languageId );

				if( regionName != null ) {
					result = string.Format( "{0}, {1}", result, regionName.Value );
				}
			}


			return result;
		}

		private async Task<bool> ValidateTrack( string track ) {
			StringBuilder requestBuilder = new StringBuilder();

			int languageId = ( await DataContext.Languages.SingleAsync( x => x.IsDefault ) ).LanguageId;
			List<string> citiesNodes = track.Split( '-' )
			.Select( trackNumber =>
				GetCityNameAndRegionName(
					int.Parse( trackNumber ),
					languageId
				)
			).ToList();

			requestBuilder.Append( @"https://maps.googleapis.com/maps/api/distancematrix/json?" );
			requestBuilder.Append( "origins=" );
			for( int i = citiesNodes.Count - 2; i >= 0; i-- ) {
				requestBuilder.Append( citiesNodes[i] );
				if( i > 0 ) {
					requestBuilder.Append( "|" );
				}
			}

			requestBuilder.Append( "&destinations=" );
			for( int i = citiesNodes.Count - 1; i >= 1; i-- ) {
				requestBuilder.Append( citiesNodes[i] );
				if( i > 1 ) {
					requestBuilder.Append( "|" );
				}
			}
			requestBuilder.Append( "&mode=driving" );


			using( var data = new WebClient().OpenRead( requestBuilder.ToString() ) ) {
				if( data == null ) return true;

				using( var reader = new StreamReader( data ) ) {
					dynamic responce = JsonConvert.DeserializeObject( reader.ReadToEnd() );
					if( responce.status == "OK" ) {
						long reccomendedDistance = long.Parse(
							responce
							.rows[responce.rows.Count - 1]
							.elements[0]
							.distance.value.ToString()
						);

						long actualDistance = 0;

						for( int i = 0; i < responce.rows.Count; i++ ) {
							actualDistance += long.Parse(
							responce
							.rows[i]
							.elements[i]
							.distance.value.ToString()
						);
						}
						if( actualDistance < reccomendedDistance * 2 ) {
							return true;
						}
					}
				}
			}

			return false;
		}

		private string GetCityNameAndRegionName( int cityNodeId, int languageId ) {
			CityNode cityNode = ( DataContext.CityNodes
				.Include( localCityNode => localCityNode.RegionNode )
				.FirstOrDefault( localCityNode => localCityNode.CityNodeId == cityNodeId ) );
			Tuple<string, string> fullCityLangKey = new Tuple<string, string>(
				cityNode.LanguageKey,
				cityNode.RegionNode.LanguageKey
			);

			string cityName = DataContext
			.MapObjectTranslations
			.FirstOrDefault( mapObjectTranslation =>
				mapObjectTranslation.LanguageKey == fullCityLangKey.Item1
				&& mapObjectTranslation.LanguageId == languageId
			).Value.Replace( ' ', '+' );

			//Commented because google api not working correctly with regions
			//string regionName = DataContext
			//.MapObjectTranslations
			//.FirstOrDefault( mapObjectTranslation =>
			//	mapObjectTranslation.LanguageKey == fullCityLangKey.Item2
			//	&& mapObjectTranslation.LanguageId == languageId
			//).Value.Replace( ' ', '+' );

			//return string.Format( "{0}+{1}", cityName, regionName );

			return cityName;
		}

		#endregion
	}
}
