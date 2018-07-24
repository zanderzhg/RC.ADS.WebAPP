/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
     
    config.baseHref = "http://" + location.host;

    config.filebrowserImageUploadUrl = '/Upload/UploadImage';
    config.font_names = 'ËÎÌå/ËÎÌå;ºÚÌå/ºÚÌå;¿¬Ìå/¿¬Ìå;Ó×Ô²/Ó×Ô²;Î¢ÈíÑÅºÚ/Î¢ÈíÑÅºÚ;' + config.font_names;
    config.allowedContent = true;

};
