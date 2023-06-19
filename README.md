# School_Administration
View and insert student and staff information. Insertion happen through base64 string of an excel file of a fixed format.
Used MySql database.
Add your connection string in appsettings.json.
Used Azure AD to authenticate my WebApi.
provides 6 different services
1. gets all student data and staff data from DB.
2. gets student data and staff data by ID.
3. parse base 64 string and reads data and inserts data in DB after few validations.
