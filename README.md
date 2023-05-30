# WorkplaceWebService

Workplaces and Positions web service combined with Employees web service for university asignment

Each workplace has employees. The workplace resource is supplemented with an employees array and stores employee IDs

# Instructions

1. git clone https://github.com/AleksandrasJ/WorkplaceEmployee-SOAP.git
2. cd WorkplaceEmployee-SOAP
3. docker-compose up -d

# SOAP Requests

<details open>
<summary>SOAP requests</summary>
<br>

## Get all workplaces

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplacesRequest/>
   </soapenv:Body>
</soapenv:Envelope>
```

## Get workplace by ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplaceRequest>
         <_id>1</_id>
      </tem:WorkplaceRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Get all positions

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:PositionsRequest/>
   </soapenv:Body>
</soapenv:Envelope>
```

## Get position by ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:PositionRequest>
         <_id>1</_id>
      </tem:PositionRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Get all positions that belongs to workplace with ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplacePositionsRequest>
         <workplaceId>1</workplaceId>
      </tem:WorkplacePositionsRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Get position by ID that belongs to workplace with ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplacePositionRequest>
         <workplaceId>1</workplaceId>
         <id>1</id>
      </tem:WorkplacePositionRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Create new workplace

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplaceCreateRequest>
         <companyName>IBM</companyName>
         <description>At IBM, we do more than work. We create. We create as technologists, developers, and engineers. We create with our partners. We create with our competitors. If you're searching for ways to make the world work better through technology and infrastructure, software and consulting, then we want to work with you.</description>
         <industry>IT Services and IT Consulting</industry>
         <website>http://www.ibm.com</website>
         <specialities>Cloud</specialities>
         <specialities>Security</specialities>
         <employees>
            <firstName>Another</firstName>
            <lastName>Employee</lastName>
            <homeAddress>1 Street, Vilnius</homeAddress>
            <currentSalary>1000</currentSalary>
            <positionName>SoftwareEnginer</positionName>
         </employees>
         <employees>
            <firstName>Other</firstName>
            <lastName>Employee</lastName>
            <homeAddress>1 Alley, Kaunas</homeAddress>
            <currentSalary>1000</currentSalary>
            <positionName>Ceo</positionName>
         </employees>
      </tem:WorkplaceCreateRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Create new position that belongs to workplace with ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:PositionCreateRequest>
         <workplaceId>5</workplaceId>
         <positionName>"Java Developer</positionName>
         <location>Vilnius</location>
         <workTimeNorm>Full-Time</workTimeNorm>
         <description>IBM CIC Baltic Custom Development department is looking for a Full Stack developer. In this role, you will use the latest tools and technologies available to deliver state-of-the-art software. You'll be responsible for ensuring that software components are expertly designed, tested, debugged, verified, and ready for integration into IBM's best-of-breed solutions that help our clients improve their business outcomes in the global marketplace</description>
         <requirements>Solid work experience with Java 8 or higher</requirements>
         <requirements>Hands-on experience with SQL</requirements>
         <requirements>Hands-on experience with GitHub</requirements>
         <requirements>Feeling comfortable in Agile / DevOps environment</requirements>
         <salary>3600</salary>
      </tem:PositionCreateRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Edit workplace by ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplaceEditReques>
        <_id>5</_id>
         <companyName>IBM</companyName>
         <description>At IBM, we do more than work. We create. We create as technologists, developers, and engineers. We create with our partners. We create with our competitors. If you're searching for ways to make the world work better through technology and infrastructure, software and consulting, then we want to work with you.</description>
         <industry>IT Services and IT Consulting</industry>
         <website>http://www.ibm.com</website>
         <specialities>Cloud</specialities>
         <specialities>Security</specialities>
         <specialities>Systems services</specialities>
         <specialities>Resiliency services</specialities>
         <specialities>Internet of Things</specialities>
         <employees>
            <firstName>Some</firstName>
            <lastName>One</lastName>
            <homeAddress>1 Square, Vilnius</homeAddress>
            <currentSalary>1000</currentSalary>
            <positionName>HR</positionName>
         </employees>
      </tem:WorkplaceEditReques>
   </soapenv:Body>
</soapenv:Envelope>
```

## Edit position by ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:PositionEditRequest>
         <_id>5</_id>
         <positionName>Java Developer</positionName>
         <location>Vilnius</location>
         <workTimeNorm>Full-time</workTimeNorm>
         <description>IBM CIC Baltic Custom Development department is looking for a Full Stack developer. In this role, you will use the latest tools and technologies available to deliver state-of-the-art software. You'll be responsible for ensuring that software components are expertly designed, tested, debugged, verified, and ready for integration into IBM's best-of-breed solutions that help our clients improve their business outcomes in the global marketplace</description>
         <requirements>Solid work experience with Java 8 or higher</requirements>
         <requirements>Hands-on experience with SQL</requirements>
         <requirements>Hands-on experience with GitHub</requirements>
         <requirements>Feeling comfortable in Agile / DevOps environment</requirements>
         <salary>4300</salary>
      </tem:PositionEditRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Edit position by ID that belongs to workplace with ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplacePositionEditRequest>
         <workplaceId>5</workplaceId>
         <id>1</id>
         <positionName>Java Developer</positionName>
         <location>Vilnius</location>
         <workTimeNorm>Full-time</workTimeNorm>
         <description>IBM CIC Baltic Custom Development department is looking for a Full Stack developer. In this role, you will use the latest tools and technologies available to deliver state-of-the-art software. You'll be responsible for ensuring that software components are expertly designed, tested, debugged, verified, and ready for integration into IBM's best-of-breed solutions that help our clients improve their business outcomes in the global marketplace</description>
         <requirements>Solid work experience with Java 8 or higher</requirements>
         <requirements>Hands-on experience with SQL</requirements>
         <requirements>Hands-on experience with GitHub</requirements>
         <requirements>Feeling comfortable in Agile / DevOps environment</requirements>
         <salary>4300</salary>
      </tem:WorkplacePositionEditRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Delete workplace by ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplaceDeleteRequest>
         <_id>1</_id>
      </tem:WorkplaceDeleteRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Delete position by ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:PositionDeleteRequest>
         <_id>1</_id>
      </tem:PositionDeleteRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

## Delete position by ID that belongs to workplace with ID

Body:
```
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:WorkplacePositionDeleteRequest>
         <workplaceId>1</workplaceId>
         <id>1</id>
      </tem:WorkplacePositionDeleteRequest>
   </soapenv:Body>
</soapenv:Envelope>
```

</details>

# REST requests

<details open>
<summary>REST requests</summary>
<br>

## GET

URL: http://localhost:80/workplaces<br />
URL: http://localhost:80/workplaces/1<br />
URL: http://localhost:80/workplaces/1/positions<br />
URL: http://localhost:80/workplaces/1/positions/1<br />
URL: http://localhost:80/positions<br />
URL: http://localhost:80/positions/1<br />

## POST

URL: http://localhost:80/workplaces<br />
BODY: 
```
{
    "companyName": "IBM",
    "description": "At IBM, we do more than work. We create. We create as technologists, developers, and engineers. We create with our partners. We create with our competitors. If you're searching for ways to make the world work better through technology and infrastructure, software and consulting, then we want to work with you.",
    "industry": "IT Services and IT Consulting",
    "website": "http://www.ibm.com",
    "specialities": [
        "Cloud",
        "Security"
    ],
    "employees": [
        {
            "firstName": "Another",
            "lastName": "Employee",
            "birthDate": "2000-03-22T05:14:25.624Z",
            "homeAddress": "1 Street, Vilnius",
            "currentSalary": 1000,
            "positionName": "SoftwareEnginer"
        },
        {
            "firstName": "Other",
            "lastName": "Employee",
            "birthDate": "2000-03-22T05:14:25.624Z",
            "homeAddress": "1 Alley, Kaunas",
            "currentSalary": 1000,
            "positionName": "Ceo"
        }
    ]
}
```
URL: http://localhost:80/workplaces/5/positions<br />
BODY: 
```
{
    "positionName": "Java Developer",
    "location": "Vilnius",
    "workTimeNorm": "Full-time",
    "description": "IBM CIC Baltic Custom Development department is looking for a Full Stack developer. In this role, you will use the latest tools and technologies available to deliver state-of-the-art software. You'll be responsible for ensuring that software components are expertly designed, tested, debugged, verified, and ready for integration into IBM's best-of-breed solutions that help our clients improve their business outcomes in the global marketplace",
    "requirements": [
        "Solid work experience with Java 8 or higher",
        "Hands-on experience with SQL",
        "Hands-on experience with GitHub",
        "Feeling comfortable in Agile / DevOps environment"
    ],
    "salary": 3600
}
```
## PUT

URL: http://localhost:80/workplaces/5<br />
BODY: 
```
{
    "companyName": "IBM",
    "description": "At IBM, we do more than work. We create. We create as technologists, developers, and engineers. We create with our partners. We create with our competitors. If you're searching for ways to make the world work better through technology and infrastructure, software and consulting, then we want to work with you.",
    "industry": "IT Services and IT Consulting",
    "website": "http://www.ibm.com",
    "specialities": [
        "Cloud",
        "Security",
        "Systems services",
        "Resiliency services",
        "Internet of Things"
    ],
    "employees": [
        {
            "firstName": "Some",
            "lastName": "One",
            "birthDate": "2000-03-22T05:14:25.624Z",
            "homeAddress": "1 Square, Vilnius",
            "currentSalary": 1000,
            "positionName": "HR"
        }
    ]

}
```
URL: http://localhost:80/workplaces/5/positions/1<br />
BODY: 
```
{
    "positionName": "Java Developer",
    "location": "Vilnius",
    "workTimeNorm": "Full-time",
    "description": "IBM CIC Baltic Custom Development department is looking for a Full Stack developer. In this role, you will use the latest tools and technologies available to deliver state-of-the-art software. You'll be responsible for ensuring that software components are expertly designed, tested, debugged, verified, and ready for integration into IBM's best-of-breed solutions that help our clients improve their business outcomes in the global marketplace",
    "requirements": [
        "Solid work experience with Java 8 or higher",
        "Hands-on experience with SQL",
        "Hands-on experience with GitHub",
        "Feeling comfortable in Agile / DevOps environment"
    ],
    "salary": 4300
}
```
URL: http://localhost:80/positions/5
```
{
    "positionName": "Java Developer",
    "location": "Vilnius",
    "workTimeNorm": "Full-time",
    "description": "IBM CIC Baltic Custom Development department is looking for a Full Stack developer. In this role, you will use the latest tools and technologies available to deliver state-of-the-art software. You'll be responsible for ensuring that software components are expertly designed, tested, debugged, verified, and ready for integration into IBM's best-of-breed solutions that help our clients improve their business outcomes in the global marketplace",
    "requirements": [
        "Solid work experience with Java 8 or higher",
        "Hands-on experience with SQL",
        "Hands-on experience with GitHub",
        "Feeling comfortable in Agile / DevOps environment"
    ],
    "salary": 4300
}
```

## DELETE

URL: http://localhost:80/workplaces/1<br />
URL: http://localhost:80/workplaces/1/positions/1<br />
URL: http://localhost:80/positions/1<br />
</details>
