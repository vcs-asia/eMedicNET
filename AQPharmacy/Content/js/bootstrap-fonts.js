/* =========================================================
 * bootstrap-fonts.js 
 *  * =========================================================
 * Copyright 2012 Amena Designs
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================= */

!function ($) {

		WebFontConfig = {
 google: { families: [ 'Ubuntu:400,500,700:latin', 'Yanone+Kaffeesatz:400,700,200:latin', 'Kreon::latin' ] }
 };

         var wf = document.createElement('script');
         wf.src = ('https:' == document.location.protocol ? 'https' : 'http') +
             '://ajax.googleapis.com/ajax/libs/webfont/1/webfont.js';
         wf.type = 'text/javascript';
         wf.async = 'true';
         var s = document.getElementsByTagName('script')[0];
         s.parentNode.insertBefore(wf, s);
		 
	$(".kreon") .css("font-family", "'Kreon'");
	$(".ubuntu") .css("font-family", "'Ubuntu'");
	$(".yanonekaffeesatz") .css("font-family", "'Yanone-Kaffeesatz'");
		 
}(window.jQuery);