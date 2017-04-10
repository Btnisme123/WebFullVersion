/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowseUrl = '/Accset/Admin/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/Accset/Admin/js/plugins/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/Accset/Admin/js/plugins/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Accset/Admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';

    config.filebrowserFlashUploadUrl = '/Accset/Admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpLoad&type=Flash';
    CKFinder.setupCKEditor(null, '/Accset/Admin/js/plugins/ckfinder/');
};
