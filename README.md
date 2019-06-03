# HELIX Assignment

## Functional Assumpions
1. N-N relationship between Event and Product
2. PutProducts is a single point API which is designed to do the following:
- Insert/Update Event (ID, Timestamp)
- 	Insert/Update Products' details (Product Id, Name)
- 	Update the sales specific details of products sold in the event (Sales_amount, etc)

## Prerequisites
* .NET Core v2.1.11 (SDK 2.1.700 to build apps Or Runtime 2.1.11 to run apps)
*  Visual Studio 2019
*  Tools for testing web services (Postman)

## How to run the application
* Clone the repository to your working desk
* Open & Build the application solution
* Run the application in IIS Express
* If you are navigated to the Test page, the development environment is ready.

## Usage & Testing

* Turn OFF the "SSL certificate verification" option in Postman   

```python
URL: https://localhost:{running_port}/api/helix/PutProducts
Method: PUT
Request Sample
{
  "id": 1,
  "timestamp": "2019-09-09 10:28",
  "products": [
    {
      "id": 11,
      "name": "Product 22",
      "quantity": 2,
      "sale_amount": 2.22
    },
     {
      "id": 5,
      "name": "Product 33",
      "quantity": 3,
      "sale_amount": 3.33
    }
  ]
}

Response Sample
{
    "version": {
        "major": 1,
        "minor": 1,
        "build": -1,
        "revision": -1,
        "majorRevision": -1,
        "minorRevision": -1
    },
    "content": null,
    "statusCode": 200,
    "reasonPhrase": "OK",
    "headers": [],
    "requestMessage": null,
    "isSuccessStatusCode": true
}

```

```python
URL: https://localhost:{running_port}/api/helix/GetProducts
Method: GET

Response Sample
[
    {
        "eventId": 1,
        "timestamp": "2019-06-02T23:33:12.5543+08:00",
        "products": [
            {
                "eventId": 1,
                "productId": 11,
                "quantity": 2,
                "sale_amount": 2.22
            },
            {
                "eventId": 1,
                "productId": 5,
                "quantity": 3,
                "sale_amount": 3.33
            }
        ]
    }
 ]
```
## Future Improvement (in reality)
* The common logging modules could be implemented for easier tracking & monitoring. Files could be stored on the cloud storage like AWS S3
* The exception handling should be wrapped, logged and passed back to calling layers with proper messages (DAL => BAL => API service)

## Pending 
* The test suite projects for BAL, DAL and HelixAssignment (coming soon)

## Contributing
* Pull requests are welcome. If you want to discuss further on the proposed solution, do not hesitate reaching me out.

## License
None.
