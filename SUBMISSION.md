**Configuration**
* Update ApiKey value for http://www.omdbapi.com/ in appsettings.json

**How to fix "No result found for test" when search**
* Update ApiKey value for http://www.omdbapi.com/ in appsettings.json

**Tested Environment**
* VisualStudio 2019
* Latest Chrome Browser
* Windows 10

**JSON-LD data is set as this year if invalid Year is returned from http://www.omdbapi.com/?apikey=mykey&s=mytitle**
* e.g. "Year": "1961–1969",

**Following error is thought as the validator bug. https://search.google.com/structured-data/testing-tool**
* All values provided for itemListElement.item.url must point to the same page

