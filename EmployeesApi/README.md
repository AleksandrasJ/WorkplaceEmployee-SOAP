# Employee API tutorial

1. Download & install [docker]("https://www.docker.com/products/docker-desktop/")
2. Turn on `Docker Desktop`
3. Copy source code from this repository to Your computer (`Code` -> `Download ZIP` -> Extract files)
4. Open the downloaded folder (with `.sln` and `docker-compose.yml` files) 
5. In this folder, open preferred terminal (bash, cmd, PowerShell, etc.)
4. Run `docker-compose up`
5. Wait until `employeeapi` container appears and starts up
6. For UI, in your browser, navigate to `http://localhost:5000/swagger/index.html`. Endpoints are listed below.
7. With Swagger UI, you should see Employee API

## Demo

#### Employees

<details>
<summary>GET URI (get all employees)</summary>
<br>

```
http://localhost:5000/api/employees
```
</details>

<details>
<summary>GET URI (get employees by ID)</summary>
<br>

```
http://localhost:5000/api/employees/0b9dfe0a-8628-426a-a802-dc162331f1f6
```
</details>

<details>
<summary>POST URI & request body example in JSON (add new employee)</summary>
<br>

```
http://localhost:5000/api/employees
```

```
{
  "firstName": "New",
  "lastName": "Employee",
  "birthDate": "2000-03-22T05:14:25.624Z",
  "homeAddress": "string",
  "currentSalary": 1000,
  "positionName": "QA"
}
```
</details>

<details>
<summary>PUT URI & request body example in JSON (update existing employee)</summary>
<br>

```
{
  "homeAddress": "Some address 123",
  "currentSalary": 990,
  "positionName": ProductOwner
}
```
</details>

<details>
<summary>DELETE URI (delete employee by ID)</summary>
<br>

```
http://localhost:5000/api/employees/f89b1b94-58dd-492a-bdb7-f9ceff13810f
```
</details>


#### Roles

<details>
<summary>GET URI (get all roles)</summary>
<br>

```
http://localhost:5000/api/roles
```
</details>

<details>
<summary>POST URI & request body example in JSON (add new role)</summary>
<br>

```
http://localhost:5000/api/roles
```

```
{
  "position": "InformationAnalist",
  "description": "Writes user stories for developers.",
  "hoursPerWeek": 30
}
```
</details>

<details>
<summary>PUT URI & request body example in JSON (update role by ID)</summary>
<br>

```
http://localhost:5000/api/roles/598266ed-e6ad-4c0b-baaa-2b456fd0173c
```

```
{
  "position": "QA",
  "description": "Writes Web API tests and UI tests.",
  "hoursPerWeek": 30
}
```
</details>

<details>
<summary>DELETE URI (delete role by ID)</summary>
<br>

```
http://localhost:5000/api/roles/598266ed-e6ad-4c0b-baaa-2b456fd0173c
```
</details>
