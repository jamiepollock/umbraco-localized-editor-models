# Umbraco Localized Editor Models

An Umbraco package for localizing properties & tabs for Document Types, Media Types & Member Types based on the language set for an Umbraco user. All using existing Umbraco Core backoffice localization conventions.

Localized values can be set generally and also on a per type alias basis. Markdown support is also optionally included too.

[![NuGet release](https://img.shields.io/nuget/v/Our.Umbraco.LocalizedEditorModels.svg)](https://www.nuget.org/packages/Our.Umbraco.LocalizedEditorModels)
[![Our Umbraco project page](https://img.shields.io/badge/our-umbraco-orange.svg)](https://our.umbraco.org/projects/backoffice-extensions/localized-editor-models/)
[![Appveyor Build](https://ci.appveyor.com/api/projects/status/kaenhf4cy1b6ixaj?svg=true)]

## Installation

**Note:** This package supports Umbraco v7.4+.

This package can be installed via [NuGet](https://www.nuget.org/packages/Our.Umbraco.LocalizedEditorModels), [Our Umbraco](https://our.umbraco.org/projects/backoffice-extensions/localized-editor-models/) or the package manager within your Umbraco installation.

Either way two new `appSettings` entries are added to the root Web.Config of your Umbraco application.

| Settings                                                    | Values            | Default Value |
|-------------------------------------------------------------|-------------------|---------------|
| Our.Umbraco.LocalizedEditorModels:Enabled                   | True, False       | True          |
| Our.Umbraco.LocalisedEditorModels:PropertyDescriptionFormat | Default, Markdown | Default       |

In addition there is also a Dashboard view which is installed in the Developer section of the Umbraco backoffice which allows admin users to view these settings without having to check the Web.config file.

## Usage

Umbraco Localized Editor Models use the Umbraco Core `ILocalizedTextService` for all localization. This means adding values is as simple as create new entries in the `~/Config/Lang/*.user.xml`.

### Basic Usage

The following conventions should be followed:

``` xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<language>
  <area alias="property_descriptions">
    <key alias="bgColor">The background colour of the page</key>
  </area>
  <area alias="property_labels">
    <key alias="bgColor">Background Colour</key>
  </area>
  <area alias="tab_labels">
    <key alias="favoriteSettings">Favourite Settings</key>
  </area>
</language>
```

**Note:** If no values are found in the language files, the backoffice will use the values provided in the property editor.

### Advanced Usage

Umbraco Localized Editor Models provides more than 1:1 translations for the above settings. 

#### Markdown support
In addition markdown can be used within your property descriptions. This will require the `Our.Umbraco.LocalisedEditorModels:PropertyDescriptionFormat` setting is changed to `Markdown`.

``` xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<language>
  ...
  <area alias="property_descriptions">
    <key alias="pageContent">The main content of the page.
    
    For more information on content editing, check out [Umbraco.TV](https://umbraco.tv/)!
    </key>
  </area>
  ...
</language>
```

#### Content/Media/Member Type alias specific translations

All three areas (property\_descriptions, property\_labels & tab\_labels) can be prefixed with the specific type alias to provide localization just for properties or tabs on particular type.

``` xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<language>
  <area alias="property_descriptions">
    <key alias="myProperty">Default description for myProperty</key>
  </area>
  <area alias="homePage_property_descriptions">
    <key alias="myProperty">Home Page description for myProperty</key>
  </area>
</language>
```

In the above example `homePage` would get a different property description for `myProperty`. Any other page would simply recieve the default property description.

## Contributing

Found a bug? [Open a ticket](../../issues/new), be sure to check [existing tickets](../../issues) first. :)

Or if you're feeling adventurous [submit a Pull Request](../../pulls).

Want to give some feedback? check out the [Feedback forum](https://our.umbraco.org/projects/backoffice-extensions/localized-editor-models/feedback/) on Our Umbraco.

## Special Thanks

 - [Lee Kelleher](https://github.com/leekelleher/) for the markdown code.
 - [Tim Geyssens](https://github.com/TimGeyssens/) for the Transform Config Package Action.

## Licensing

Licensed under the [MIT License](License).

### Package Logo

Logo created using [translate by icon 54](https://thenounproject.com/icon/532871/) licensed under [Creative Commons](https://creativecommons.org/licenses/by/3.0/).