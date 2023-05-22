import mongoose from "mongoose";

const { Schema } = mongoose;
// mongo -> localhost
mongoose.connect('mongodb://mongo:27017/jobs', { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log('Connected to database succesfully!'))
    .catch((err) => {
        console.log('Failed to connect to database!');
        console.log(err);
    })


const workplaceSchema = new Schema({
    _id: Number,
    companyName: {
        type: String,
        required: true
    },
    description: String,
    industry: String,
    website: String,
    specialities: [String],
    refPositions: String,
    employees: [String]
});

const positionSchema = new Schema({
    _id: Number,
    id: Number,
    workplaceId: Number,
    positionName: {
        type: String,
        required: true
    },
    location: String,
    workTimeNorm: String,
    description: String,
    requirements: [String],
    salary: Number
});

const Workplace = mongoose.model('Workplace', workplaceSchema);
const Position = mongoose.model('Position', positionSchema);

await Workplace.insertMany([
    {
        _id: 1, companyName: "Teltonika", description: "Easy Key to IoT", industry: "Telecommunications",
        website: "http://www.teltonika.lt", specialities: ["Transport telematics", "Integrated solutions", "Fleet management"],
        refPositions: "/workplaces/1/positions", employees: ["9ed82bac-3d01-485b-ad62-ce7caf082583", "2d2218bb-9ea0-47ba-b2a1-20b6c6e99450"]
    },
    {
        _id: 2, companyName: "Swedbank", description: "Financial Services", industry: "Financial Services",
        website: "http://www.swedbank.com", specialities: ["Financial services", "Mortgage lending", "Private banking"],
        refPositions: "/workplaces/2/positions", employees: ["0b9dfe0a-8628-426a-a802-dc162331f1f6", "f89b1b94-58dd-492a-bdb7-f9ceff13810f"]
    },
    {
        _id: 3, companyName: "Lietuvos Geležinkeliai", description: "LTG yra krovinių ir keleivių vežimo bei viešosios geležinkelių infrastruktūros valdymo įmonių grupė",
        industry: "Rail Transportation", website: "http://www.litrail.lt", specialities: ["manage (maintain, renew) public railway infrastruture", "ensure positive return on equity"],
        refPositions: "/workplaces/3/positions", employees: ["0d1e2ad0-4712-41cb-98a8-eb3b617f7e74"]
    },
    {
        _id: 4, companyName: "Revolut", description: "One app, all things money", industry: "Financial Services",
        website: "https://www.revolut.com", specialities: ["Mobile Banking", "Card Payments", "Money Remittance"],
        refPositions: "/workplaces/4/positions", employees: ["13486202-30c4-4eaa-8f13-f52ee0541b54"]
    }

]).then(result => {
}).catch(err => {
});

await Position.insertMany([
    {
        _id: 1, id: 1, workplaceId: 1, positionName: "Senior Developer", location: "Vilnius", workTimeNorm: "Full-time",
        description: "We are proud of being a group of companies working on made-in-Lithuania products and currently looking for a Mid/Senior Backend Developer who will play an important role towards creation of innovative solutions.",
        requirements: ["Have minimum of 4 years experience in PHP", "Experience in working with MySQL", "In depth understanding of API paradigms"],
        salary: 4200
    },
    {
        _id: 2, id: 2, workplaceId: 1, positionName: "Node.JS Engineer", location: "Vilnius", workTimeNorm: "Full-time",
        description: "We are currently inviting an ambitious Node.JS Engineer to join our growing R&D team in Vilnius, Lithuania and support the Teltonika Telemedic team.",
        requirements: ["Have at least 3 years of experience with NodeJS", "Have a knowledge how to create clean and structured Node.js project. Ability to maintain it that way"],
        salary: 4300
    },
    {
        _id: 3, id: 1, workplaceId: 2, positionName: "Junior Java software engineer", location: "Vilnius", workTimeNorm: "Full-time",
        description: "We are looking for Java Developer who wants to be a part of our agile team that works closely with Swedish Banking to develop efficient tools for pricing of products in digital channels.",
        requirements: ["Bachelor’s or Master’s degree in a related field, or similar working experience", "Some experience with Java", "Some in SQL and Oracle database"],
        salary: 1900
    },
    {
        _id: 4, id: 1, workplaceId: 4, positionName: "Quantitative Analyst", location: "Vilnius", workTimeNorm: "Full-time",
        description: "We're looking for an exceptional Quantitative Analyst to work with the CIO and Portfolio Management Product team who invest in and trade a range of fixed income and derivative products.",
        requirements: ["3-8 years of professional quant experience related to Fixed Income and derivatives valuation (IR and FX preferred), including model development", "Solid technical programming skills; Python, Java, C++ preferred, but other languages will be considered"],
        salary: 4600
    }
]).then(result => {
}).catch(err => {
});

export { Workplace, Position };