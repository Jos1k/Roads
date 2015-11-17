using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Roads.Common.Models;
using Roads.Common.Models.DataContract;
using Roads.Common.Repositories;
using Roads.DataBase.Model.Models;

namespace Roads.Common.Managers
{
    /// <summary>
    /// The manager for Find Road functionality. 
    /// </summary>
    public class RoadsManager : IDisposable
    {
        #region Private fields and constants

        private int _searchingDepth;

        private const char Separator = '-';

        private List<string> _roads;

        /// <summary>
        /// The _map object translations repository
        /// </summary>
        private readonly RoutesRepository _routeRepository;

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSuggestionsManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public RoadsManager(RoutesRepository repository)
        {
            _routeRepository = repository;

            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartSuggestionsManager"/> class.
        /// </summary>
        public RoadsManager()
        {
            _routeRepository = new RoutesRepository();

            Initialize();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the searching depth.
        /// </summary>
        /// <returns>Returns searching depth</returns>
        public int GetSearchingDepth()
        {
            return _routeRepository.GetSearchingDepth();
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <param name="startedCityId">The started city identifier.</param>
        /// <param name="destinationCityId">The destination city identifier.</param>
        public List<string> BuildRoutes(int startedCityId, int destinationCityId)
        {
            _roads = new List<string>();

            if (startedCityId == destinationCityId || startedCityId == 0 || destinationCityId == 0)
            {
                return new List<string>();
            }

            var beginPointId = startedCityId < destinationCityId ? startedCityId : destinationCityId;

            var endPointId = destinationCityId > startedCityId ? destinationCityId : startedCityId;

            var pointTree = new PointTree
            {
                ParentId = beginPointId,
                ChildPointIds = _routeRepository.GetAllRelationsIds(beginPointId)
            };

            Build(string.Empty, ref pointTree, endPointId);

            return _roads;
        }

        /// <summary>Gets the route estimation.</summary>
        /// <param name="route">The route string value.</param>
        /// <returns>The <see cref="RouteEstimation"/> result.</returns>
        public RouteEstimation GetRouteEstimation(string route)
        {
            //To do: route string value can be changed on hash value and gat the route from data base.
            var estimation = new RouteEstimation
            {
                CityPointsIds = route.Split(Separator).Select(int.Parse).ToArray(),
            };

            for (var i = 0; i < estimation.CityPointsIds.Count() - 1; i++)
            {
                var routeNode = _routeRepository.GetRouteNode(
                    GetPointId(true, estimation.CityPointsIds[i], estimation.CityPointsIds[i + 1]),
                    GetPointId(false, estimation.CityPointsIds[i], estimation.CityPointsIds[i + 1])
                    );

                if (routeNode != null)
                {
                    estimation.NodesEstimations.Add(GetEstimationForRouteNode(routeNode));
                }
            }

            return estimation;
        }

        /// <summary>
        /// Gets the feedback controls for new feedback.
        /// </summary>
        /// <returns>GetLanguageById list of feedback controls for new feedback</returns>
        public List<HolisticFeedback> GetFeedbackControlsForNewFeedback()
        {
            var feebackControls = new List<HolisticFeedback>();

            foreach (FeedbackItem feedbackItem in _routeRepository.GetFeedbackControlsForNewFeedback())
            {
                var feedbackControlItem = new HolisticFeedback();

                feedbackControlItem.Value = null;
                feedbackControlItem.UserName = String.Empty;
                feedbackControlItem.FeedbackItem = new FeedbackItemData()
                {
                    FeedbackItemId = feedbackItem.FeedbackItemId,
                    IsNumeric = feedbackItem.IsNumeric,
                    Mandatory = feedbackItem.Mandatory,
                    SortNumber = feedbackItem.SortNumber,
                    FeedbackModelId = feedbackItem.FeedbackModelId,
                    DescriptionTranslationKey = feedbackItem.DescriptionTranslationKey,
                    NameTranslationKey = feedbackItem.NameTranslationKey
                };
                feedbackControlItem.FeedbackModel = new FeedbackModelData()
                {
                    FeedbackModelId = feedbackItem.FeedbackModel.FeedbackModelId,
                    HtmlCode = feedbackItem.FeedbackModel.HtmlCode,
                    JavascriptCode = feedbackItem.FeedbackModel.JavascriptCode
                };
                feebackControls.Add(feedbackControlItem);
            }
            return feebackControls;
        }

        /// <summary>
        /// Gets the route details by hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="language"></param>
        /// <returns>Return RouteDetailsData object.</returns>
        public RouteDetailsData GetRouteDetailsByHash(string hash, string language)
        {
            if (hash == null)
            {
                throw new ArgumentNullException("hash");
            }
            if (language == null)
            {
                throw new ArgumentNullException("language");
            }

            var routeDetailsData = new RouteDetailsData();
            var trek = _routeRepository.GetTrekByHash(hash);
            if (trek != null)
            {
                string[] cityPoints = trek.Track.Split('-');
                var nodes = new List<RouteNode>();
                for (int i = 0; i < (cityPoints.Length - 1); i++)
                {
                    long pointOrigin;
                    long pointDestination;
                    if (Int64.TryParse(cityPoints[i], out pointOrigin) &&
                        Int64.TryParse(cityPoints[i + 1], out pointDestination))
                    {
                        var newItem = _routeRepository.GetRouteNode(pointOrigin, pointDestination);
                        if (newItem == null)
                        {
                            newItem = _routeRepository.GetRouteNode(pointDestination, pointOrigin);
                            if (newItem != null)
                            {
                                var destCityNode = new CityNode
                                {
                                    CityNodeId = newItem.DestinationCityNode.CityNodeId,
                                    DestinationRouteNodes = newItem.DestinationCityNode.DestinationRouteNodes,
                                    DestinationTreks = newItem.DestinationCityNode.DestinationTreks,
                                    LanguageKey = newItem.DestinationCityNode.LanguageKey,
                                    OriginRouteNodes = newItem.DestinationCityNode.OriginRouteNodes,
                                    OriginTreks = newItem.DestinationCityNode.OriginTreks,
                                    RegionNode = newItem.DestinationCityNode.RegionNode,
                                    RegionNodeId = newItem.DestinationCityNode.RegionNodeId
                                };
                                int destCityNodeId = newItem.DestinationCityNodeId;
                                var origenCityNode = new CityNode
                                {
                                    CityNodeId = newItem.OriginCityNode.CityNodeId,
                                    DestinationRouteNodes = newItem.OriginCityNode.DestinationRouteNodes,
                                    DestinationTreks = newItem.OriginCityNode.DestinationTreks,
                                    LanguageKey = newItem.OriginCityNode.LanguageKey,
                                    OriginRouteNodes = newItem.OriginCityNode.OriginRouteNodes,
                                    OriginTreks = newItem.OriginCityNode.OriginTreks,
                                    RegionNode = newItem.OriginCityNode.RegionNode,
                                    RegionNodeId = newItem.OriginCityNode.RegionNodeId
                                };
                                int origenCityNodeId = newItem.OriginCityNodeId;

                                newItem.OriginCityNodeId = destCityNodeId;
                                newItem.OriginCityNode = destCityNode;
                                newItem.DestinationCityNodeId = origenCityNodeId;
                                newItem.DestinationCityNode = origenCityNode;
                            }
                        }
                        nodes.Add(newItem);
                    }
                    else
                    {
                        nodes.Add(null);
                    }
                }
                foreach (var routeNode in nodes)
                {
                    var rdFeedbacks = _routeRepository.GetRoutDetailsFeedbackFor(routeNode, language);

                    routeDetailsData.RouteDetailsItems.Add(rdFeedbacks);
                }
            }
            return routeDetailsData;
        }

        /// <summary>
        /// Gets the hash for route.
        /// </summary>
        /// <param name="trek">The trek.</param>
        /// <returns>Hash value for route.</returns>
        public string GetHashForRoute(string trek)
        {
            return string.Format("{0:X8}", trek.GetHashCode());
        }

        /// <summary>
        /// Gets the routes search result.
        /// </summary>
        /// <param name="startedCityId">The started city identifier.</param>
        /// <param name="destinationCityId">The destination city identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="languageName">Name of the language.</param>
        /// <returns>
        /// The <see cref="RoutesSearchResultData" /> result.
        /// </returns>
		public async Task<RoutesSearchResultData> GetRoutesSearchResult( int startedCityId, int destinationCityId, int page, string languageName, bool isRouteValidation )
        {
            BuildRoutes(startedCityId, destinationCityId);

            var result = await _routeRepository.GetRoutesSearchResultData(
                GetPointId(true, startedCityId, destinationCityId),
                GetPointId(false, startedCityId, destinationCityId),
                page,
				languageName, isRouteValidation );

            return result;
        }

        /// <summary>
        /// Adds the new feedback and get URL to route.
        /// </summary>
        /// <param name="routesNodeWithFeedbacksData">The routes node with feedbacks data.</param>
        /// <returns>Return URL to route with new feedbacks</returns>
        public string AddNewFeedbackAndGetUrlToRoute(List<RouteNodeWithFeedbacksData> routesNodeWithFeedbacksData)
        {
            RoutesRepository routeRepository = new RoutesRepository();
            FeedbackItemRepository feedbackItemRepository = new FeedbackItemRepository();
            StringBuilder track = new StringBuilder(routesNodeWithFeedbacksData.Count * 5 + routesNodeWithFeedbacksData.Count - 1);

            bool backwardOrder = routesNodeWithFeedbacksData[0].OriginCityNodeId >
                     routesNodeWithFeedbacksData[routesNodeWithFeedbacksData
                     .Count - 1].DestinationCityNodeId;

            routesNodeWithFeedbacksData.ForEach(routeWithFeedback =>
            {
                RouteNode routeNode = routeRepository.GetRouteNode(
                routeWithFeedback.OriginCityNodeId, routeWithFeedback.DestinationCityNodeId) ??
                    new RouteNode()
                    {
                        RouteNodeId = routeRepository.CreateRouteNode(routeWithFeedback.OriginCityNodeId,
                            routeWithFeedback.DestinationCityNodeId)
                    };

                int feedbackId = feedbackItemRepository.AddNewFeedback(routeNode.RouteNodeId, routeWithFeedback.UserId,
                    routeWithFeedback.SubmitTime);

                routeWithFeedback.FeedbackValues.ForEach(feedbackValue => feedbackItemRepository.AddNewFeedbackValue(new FeedbackValueData()
                {
                    FeedbackId = feedbackId,
                    Value = feedbackValue.Value,
                    FeedbackItemId = feedbackValue.FeedbackItemId
                }));

                if (!backwardOrder)
                {
                    track.Append(routeWithFeedback.OriginCityNodeId).Append("-");
                    if (routeWithFeedback.Equals(routesNodeWithFeedbacksData.Last()))
                    {
                        track.Append(routeWithFeedback.DestinationCityNodeId);
                    }
                }
                else
                {
                    track.Insert(0, routeWithFeedback.OriginCityNodeId).Insert(0, "-");
                    if (routeWithFeedback.Equals(routesNodeWithFeedbacksData.Last()))
                    {
                        track.Insert(0, routeWithFeedback.DestinationCityNodeId);
                    }
                }
            });
            BuildRoutes(routesNodeWithFeedbacksData[0].OriginCityNodeId,
                routesNodeWithFeedbacksData[routesNodeWithFeedbacksData.Count - 1].DestinationCityNodeId);

            string hash = routeRepository.GetHashByTrack(track.ToString());
            return String.Format("{0}-{1}-To-{2}", hash,
                routesNodeWithFeedbacksData.First().OriginCityNode,
                routesNodeWithFeedbacksData.Last().DestinationCityNode)
                .Replace(" ", "-")
                .Replace(",", "");
        }

        /// <summary>
        /// Creates the feedback.
        /// </summary>
        /// <param name="feedbacksData">The feedbacks data.</param>
        public void CreateFeedback(RouteNodeWithFeedbacksData feedbacksData)
        {
            var feedbackItemRepository = new FeedbackItemRepository();
            var routeRepository = new RoutesRepository();

            RouteNode routeNode = routeRepository.GetRouteNode(
                feedbacksData.OriginCityNodeId, feedbacksData.DestinationCityNodeId);

            int feedbackId = feedbackItemRepository.AddNewFeedback(routeNode.RouteNodeId, feedbacksData.UserId,
                    feedbacksData.SubmitTime);

            feedbacksData.FeedbackValues.ForEach(feedbackValue => feedbackItemRepository.AddNewFeedbackValue(new FeedbackValueData()
            {
                FeedbackId = feedbackId,
                Value = feedbackValue.Value,
                FeedbackItemId = feedbackValue.FeedbackItemId
            }));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the route.
        /// </summary>
        /// <param name="trek">The trek.</param>
        private void SaveRoute(string trek)
        {
            _routeRepository.AddTrekIfNotExist(new Trek
            {
                Track = trek,
                Hash = GetHashForRoute(trek),
                OriginCityNodeId = GetPointId(true, trek),
                DesinationCityNodeId = GetPointId(false, trek),
                NodesCount = (byte)trek.Split('-').Count(),
                TrekDate = DateTime.Now
            });
        }

        /// <summary>
        /// Gets the point identifier.
        /// </summary>
        /// <param name="isBegin">if set to <c>true</c> [is begin].</param>
        /// <param name="pointId1">The point id1.</param>
        /// <param name="pointId2">The point id2.</param>
        /// <returns>Point Id.</returns>
        private int GetPointId(bool isBegin, int pointId1, int pointId2)
        {
            if (isBegin)
            {
                return pointId1 < pointId2
                    ? pointId1
                    : pointId2;
            }

            return pointId1 > pointId2
                ? pointId1
                : pointId2;
        }

        /// <summary>
        /// Gets the point identifier.
        /// </summary>
        /// <param name="isBegin">if set to <c>true</c> [is begin].</param>
        /// <param name="trek">The trek.</param>
        /// <returns>Point Id.</returns>
        private int GetPointId(bool isBegin, string trek)
        {
            if (!string.IsNullOrEmpty(trek))
            {
                var points = trek.Split(Separator).Select(int.Parse).ToList();

                return isBegin ? points.First() : points.Last();
            }

            return 0;
        }

        /// <summary>
        /// Gets the estimation for route node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The <see cref="RouteNodeEstimation"/> estimation.</returns>
        private RouteNodeEstimation GetEstimationForRouteNode(RouteNode node)
        {
            return new RouteNodeEstimation
            {
                RouteNode = new RouteNodeData
                {
                    DestinationCityNodeId = node.DestinationCityNodeId,
                    OriginCityNodeId = node.OriginCityNodeId,
                    RouteNodeId = node.RouteNodeId
                },

                HolisticFeedbacks = _routeRepository.GetHolisticFeedbackFor(node, null)
            };
        }

        /// <summary>
        /// Build the specified trek.
        /// </summary>
        /// <param name="trek">The string formatted trek.</param>
        /// <param name="step">The <see cref="PointTree"/> instance.</param>
        /// <param name="destinationCityId">The destination city identifier.</param>
        private void Build(string trek, ref PointTree step, long destinationCityId)
        {
            trek = string.IsNullOrEmpty(trek) ? step.ParentId.ToString(CultureInfo.InvariantCulture) : string.Format("{0}-{1}", trek, step.ParentId);

            if (step.ParentId == destinationCityId)
            {
                if (_roads.Count(s => s == trek) == 0)
                {
                    _roads.Add(trek);

                    SaveRoute(trek);
                }
            }
            else
            {
                if (CheckTrekDepth(trek))
                {

                    while (step.ChildPointIds.Count != 0)
                    {
                        var nexPointId = step.ChildPointIds.First();

                        var newLevel = new PointTree
                        {
                            ParentId = nexPointId,
                            ChildPointIds = GetRelations(trek, nexPointId)
                        };

                        step.ChildPointIds.Remove(nexPointId);

                        Build(trek, ref newLevel, destinationCityId);
                    }
                }
            }
        }

        /// <summary>
        /// Gets GetRelations.
        /// </summary>
        /// <param name="trek">The trek.</param>
        /// <param name="id">The id.</param>
        /// <returns>List of relation id's.</returns>
        private List<int> GetRelations(string trek, int id)
        {
            var visited = trek.Split(Separator).ToList().Select(int.Parse).ToList();

            var relations = _routeRepository.GetAllRelationsIds(id);

            relations = relations.Where(e => !visited.Contains(e)).ToList();

            return relations;
        }

        /// <summary>
        /// Checks the trek deeps.
        /// </summary>
        /// <param name="trek">The trek.</param>
        /// <returns> The <see cref="bool"/> value true if trek deep no biggest then <see cref="_searchingDepth"/> value.</returns>
        private bool CheckTrekDepth(string trek)
        {
            return trek.Split(Separator).Count() <= _searchingDepth;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _searchingDepth = _routeRepository.GetSearchingDepth();
        }

        #endregion

        #region IDisposable implemetation

        public void Dispose()
        {
        }

        #endregion
    }
}