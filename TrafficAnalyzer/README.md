# Traffic Analyzer

Traffic Analyzer makes it possible to easily review your IIS server logs against crawler originated traffic, so that you can take necessary steps for your site to account for them in future.

## Technologies used

Logs parsing is based on [Log Parser 2.2](http://www.microsoft.com/en-us/download/details.aspx?id=24659) component from Microsoft. It provides programmatic SQL like interface to querying IIS logs from given file(s) or folder(s). Given its capabilities it was used  not only to do mere parsing but also initial aggregation of data. Except from providing standard, well-known way of accessing logs, it also gives the abstraction over how logs are being stored physically (single file per day or multiple ones), so that application itself does not need to care about it.

Data access is based on [Simple.Data](http://simplefx.org/simpledata/docs/) from Mark Rendle. Through heavy usage of dynamic types support in .NET, it provides fluent and straight forward way for querying database. Since we only do bulk inserts and reading of a single database table, ORM usage would not be justified due to identity / session management overhead it brings. The library is used through out the solution for both Tool and Web component. Database schema is maintain through Database project published (for development purposes) to LOCAL DB.

On Web side ASP.NET MVC 5.0 was used alongside with a few standard Java Script libraries: Bootstrap, jQuery and DataTables, which together allow to create multi-browser responsive UI easily.

For testing [XUnit](https://xunit.github.io/) framework alongside with [FluentAssertions](http://www.fluentassertions.com/) has been used.

##Architecture

System consists of two main parts. First is a tool that should be scheduled to run nightly (e.g. using Windows Scheduler). It will parse and analyze the traffic from past 24 hours and upload it into the database. The second one is a web application that allows you to review the data from there.

### Tool
Tool on its own is designed as a multi-step process. First it pulls aggregated data from actual logs within expected time window (`ILogQueries`). Then it processes those entries (`ICrawlerTrafficProcessor`) to determine and keep only known crawlers (`ICrawlerDetector`). Finally it uploads previous steps results into a database (`ILogStorage`).

> **Note:**
> Tool assumes IIS logs are kept in [W3C extended format](http://www.w3.org/TR/WD-logfile.html) and include the following columns `c-ip`, `cs(User-Agent)`,  `cs-bytes`, and `sc-bytes`. In case of different format alternative `ILogQueries` implementation would need to be created.

> **Important:**
> You configure where your logs are placed through `iis.logs.path` from `appSettings`.

Crawler processing purpose is two-fold. Most importantly it detects which entries are supsected crawlers. Currently detection is based ultimately on User-Agent string but this is potential improvement point. Non-crawler entries will get filtered out from the result set. Furthermore since many bots use different User-Agent string, we could end up with "duplicated" entries after this phase. To avoid that 2nd-level aggregation takes place and the data eventually inserted into the storage will contain unique entries only.

### Database
Database access is designed as a separate Shared component to achieve high cohesion of data manipulation routines. Since both Tool and Web parts leverage the same database, in case any of those changes, the second would be affected. By having all in one place potential change is easier to recognize and apply in all required operations.

### Web site
Web site leverages DataTables for tabular presentation with sorting, filtering and paging handled on the client side. While it requires full data load on page display, the data is already fully prepared and is expected to be of limited size since we aggregate per crawler. While we have fixed number of recognized bots, upper bound on unique originating IPs cannot be placed. However the query made from Web dashboard is simple select and we will gain the fast application behavior once the intial load is complete. It will allow user to use dashboard in faster manner and may reduce SQL server load since less SQL queries will need to be processed globally. 
