# Top Style API

- [Top Style API](#Top-Style-api)
  - [Auth](#auth)
    - [Register](#register)
      - [Register Request](#register-request)
      - [Register Response](#register-response)
    - [Login](#login)
      - [Login Request](#login-request)
      - [Login Response](#login-response)
  - [Products](#products)
    - [Search](#search)
      - [Search Request](#search-request)
      - [Search Response](#search-response)
    - [CreateProduct](#CreateProduct)
      - [CreateProduct Request](#CreateProduct-request)
      - [CreateProduct Response](#CreateProduct-response)
    - [UpdateProduct](#UpdateProduct)
      - [UpdateProduct Request](#UpdateProduct-request)
      - [UpdateProduct Response](#UpdateProduct-response)
    - [Deactivate](#Deactivate)
      - [Deactivate Request](#Deactivate-request)
      - [Deactivate Response](#Deactivate-response)
    - [Remove](#Remove)
      - [Remove Request](#Remove-request)
      - [Remove Response](#Remove-response)

## Auth

### Register

```js
POST /auth/register
```

#### Register Request

```json
{
    "email": "Felix@lorem.com",
    "password": "Lorem1232!"
}
```

#### Register Response

```js
200 OK
```

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "email": "Felix@Lorem.com",
  "token": "eyJhb..z9dqcnXoY"
}
```

### Login

```js
POST /auth/login
```

#### Login Request

```json
{
    "email": "amichai@mantinband.com",
    "password": "Amiko1232!"
}
```

```js
200 OK
```

#### Login Response

```json
{
  "id": "d89c2d9a-eb3e-4075-95ff-b920b55aa104",
  "email": "Felix@Lorem.com",
  "token": "eyJhb..hbbQ"
}
```

## Products

### Search

```js
GET /api/products?search={searchTerm}&category={category}
```

#### Search Request

```json
/api/products?search=blue&category=shirts
```
#### Search Response
```js
201 (Created)
```

```json
[
  {
    "name": "Striped Shirt",
    "description": "A stylish striped shirt",
    "price": 39.99,
    "category": "Shirts"
  },
  ...
]

```
### CreateProduct

```js
GET /api/products
```

#### CreateProduct Request

```json
{
  "name": "Product Name",
  "description": "Product Description",
  "price": 9.99,
  "category": "Category Name"
}

```
#### CreateProduct Response
```js
201 (Created)
```

```json
{
  "id": "product-id",
  "name": "Product Name",
  "description": "Product Description",
  "price": 9.99,
  "category": "Category Name"
}

```
### UpdateProduct

```js
GET /api/products
```

#### UpdateProduct Request

```json
{
  "name": "Updated Product Name",
  "description": "Updated Product Description",
  "price": 12.99,
  "category": "Updated Category Name"
}

```

#### UpdateProduct Response
```js
204 (No Content)
```


### Deactivate Product

```js
DELETE /api/products/{id}
```


#### Deactivate Response
```js
204 (No Content)
```


### Remove Product

```js
GET /api/products/{id}/remove
```


#### Remove Response
```js
204 (No Content)
```