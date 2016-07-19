#Face Identification in Photos using OpenCV#

Now face identification has become ubiquitous in most of the cloud photo storage providers. Memom.com during its inception wanted to explore the possibility of such function in its service offering and this challenge was to identify the feasibility and easy integration as SaaS platform to mobile app. 

[OpenCV](http://opencv.org/) provides libraries for image processing and face identification in photos. There is c# wrapper for OpenCV provided by [Emgu](http://www.emgu.com/wiki/index.php/Main_Page) that fits the bill to quickly build a PoC to demonstrate the face identification tech delivered through a ASP.NET MVC5.2 web app. 

This [responsive web app](http://memomface.cloudapp.net/) incorporates a basic registration and login along with photo album function that includes member tagging and photo upload. Source code provided here is a complete MS VS 2013 solution with all modules required to re-create the web app yourself. Mvc Web App uses Data Repository pattern for data access and PagedList for results paging.

After registration and successfully logging into app, one has to identify the family members for the service to accurately tag all photos. For this, every family member details are entered along with a group photo wherein the service identifies faces in photo and allows which face as member's face. And subsequently any albums created, all photos uploaded will be tagged for this face. This web app requires further improvements as to displaying tagged photos as a group and search functionality.

The start feature is the backend batch program that can tag photos through a batch that runs periodically calling the OpenCV functions off a SQL Server table that makes note of new photos uploaded for albums created. 