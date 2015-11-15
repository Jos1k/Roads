
SET IDENTITY_INSERT [dbo].[FeedbackModels] ON 
INSERT [dbo].[FeedbackModels] ([FeedbackModelId], [HtmlCode], [JavascriptCode], [FeedbackModelName]) VALUES (1, '<b><label id="FeedbackTextControl">Title</label></b><div class="TxtRtr"><textarea class="txtMult" rows="10" style="width:100%;resize:vertical;"></textarea></div>', 'function getValue() {return String(jQuery(''.txtMult'').val());} function setValue(sValue) {jQuery(''.txtMult'').val(String(sValue));}','Text Area Control')
INSERT [dbo].[FeedbackModels] ([FeedbackModelId], [HtmlCode], [JavascriptCode], [FeedbackModelName]) VALUES (2, '<style type="text/css">
 .StrRtr .star_rating {
  font-size: 0;
  white-space: nowrap;
  display: inline-block;
  width: 250px;
  height: 50px;
  overflow: hidden;
  position: relative;
    background: url("/../../../Content/Images/bookmarkdisabled.png");
  background-size: contain;
}
.StrRtr .star_rating i {
  opacity: 0;
  position: absolute;
  left: 0;
  top: 0;
  height: 100%;
  width: 20%;
  z-index: 1;
    background: url("/../../../Content/Images/bookmark.png");
  background-size: contain;
}
.StrRtr .star_rating input {
  -moz-appearance: none;
  -webkit-appearance: none;
  opacity: 0;
  display: inline-block;
  width: 20%;
  height: 100%;
  margin: 0;
  padding: 0;
  z-index: 2;
  position: relative;
}
.StrRtr .star_rating input:hover + i,
.StrRtr .star_rating input:checked + i {
  opacity: 1;
}
.StrRtr .star_rating i ~ i {
  width: 40%;
}
.StrRtr .star_rating i ~ i ~ i {
  width: 60%;
}
.StrRtr .star_rating i ~ i ~ i ~ i {
  width: 80%;
}
.StrRtr .star_rating i ~ i ~ i ~ i ~ i {
  width: 100%;
}
.StrRtr .choice {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  text-align: center;
  padding: 20px;
  display: block;
}
.StrRtr {
  font-family: "HelveticaNeue-Light", "Helvetica Neue Light", "Helvetica Neue", Helvetica, Arial, "Lucida Grande", sans-serif;
}
.StrRtr::before {
  height: 100%;
  content: '''';
  width: 0;
  background: red;
  vertical-align: middle;
  display: inline-block;
} </style>
<b><label id="FeedbackTextControl">Title</label></b> 
<div class="StrRtr">
<span class="star_rating">
  <input type="radio" name="StrRating" value="1" id="rating_1"><i></i>
  <input type="radio" name="StrRating" value="2" id="rating_2"><i></i>
  <input type="radio" name="StrRating" value="3" id="rating_3"><i></i>
  <input type="radio" name="StrRating" value="4" id="rating_4"><i></i>
  <input type="radio" name="StrRating" value="5" id="rating_5"><i></i>
</span>
</div>', 
'function getValue() {
  return String(jQuery(''input[name=StrRating]:checked'').val()); } function setValue(sValue){if(String(sValue)&&String(sValue)!=''undefined''){jQuery(''input[name=StrRating][value='' + String(sValue) + '']'').prop(''checked'', true);} else {jQuery(''input[name=StrRating]'').prop(''checked'', false);}}   ', 'Five star control')
SET IDENTITY_INSERT [dbo].[FeedbackModels] OFF