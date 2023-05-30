import { Position, Workplace } from "./database.js";
import axios from 'axios';

// SOAP 
async function getWorkplace(args) {
    let workplace = await Workplace.findOne(args);

    let ids = [];
    let employeesArray = [];

    workplace = workplace.toJSON()

    ids = workplace.employees;
    workplace.employees = [];

    for (let id of ids) {
        await axios.get(`http://employees:80/api/employees/${id}`, { timeout: 1000 }).then(response => {
            let employeeBody = {
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                homeAddress: response.data.homeAddress,
                currentSalary: response.data.currentSalary,
                position: response.data.role.position
            };
            employeesArray.push(employeeBody);
        }).catch(error => {
        });
    }

    return {
        _id: workplace._id,
        companyName: workplace.companyName,
        description: workplace.description,
        industry: workplace.industry,
        website: workplace.website,
        specialities: workplace.specialities,
        employees: employeesArray
    };
}

async function getWorkplaces() {
    let workplaces = await Workplace.find();
    let returnArr = [];

    for (let workplace of workplaces) {
        let ids = [];
        let employeesArray = [];

        workplace = workplace.toJSON()

        ids = workplace.employees;
        workplace.employees = [];

        for (let id of ids) {
            await axios.get(`http://employees:80/api/employees/${id}`, { timeout: 1000 }).then(response => {
                let employeeBody = {
                    firstName: response.data.firstName,
                    lastName: response.data.lastName,
                    homeAddress: response.data.homeAddress,
                    currentSalary: response.data.currentSalary,
                    position: response.data.role.position
                };
                employeesArray.push(employeeBody);
            }).catch(error => {
            });
        }

        returnArr.push({
            _id: workplace._id,
            companyName: workplace.companyName,
            description: workplace.description,
            industry: workplace.industry,
            website: workplace.website,
            specialities: workplace.specialities,
            employees: employeesArray
        });
    }
    return returnArr;
}

async function getPosition(args) {
    let position = await Position.findOne(args);
    return {
        _id: position._id,
        workplaceId: position.workplaceId,
        positionName: position.positionName,
        location: position.location,
        workTimeNorm: position.workTimeNorm,
        description: position.description,
        requirements: position.requirements,
        salary: position.salary
    };
}

async function getPositions() {
    let positions = await Position.find();
    let returnArr = [];
    positions.forEach((position) => {
        returnArr.push({
            _id: position._id,
            workplaceId: position.workplaceId,
            positionName: position.positionName,
            location: position.location,
            workTimeNorm: position.workTimeNorm,
            description: position.description,
            requirements: position.requirements,
            salary: position.salary
        });
    });
    return returnArr;
}

async function getWorkplacePositions(args) {
    let positions = await Position.find(args);
    let returnArr = [];
    positions.forEach((position) => {
        returnArr.push({
            id: position.id,
            workplaceId: position.workplaceId,
            positionName: position.positionName,
            location: position.location,
            workTimeNorm: position.workTimeNorm,
            description: position.description,
            requirements: position.requirements,
            salary: position.salary
        });
    });
    return returnArr;
}

async function getWorkplacePosition(args) {
    let position = await Position.findOne(args);
    return {
        id: position.id,
        workplaceId: position.workplaceId,
        positionName: position.positionName,
        location: position.location,
        workTimeNorm: position.workTimeNorm,
        description: position.description,
        requirements: position.requirements,
        salary: position.salary
    };
}

async function deleteWorkplace(args) {
    await Workplace.findOneAndRemove(args);
    let id = args._id;
    let length = 0;

    await Position.find({ workplaceId: id }).then(result => {
        length = result.length;
    });

    for (let i = 0; i < length; ++i) {
        await Position.findOneAndRemove({ workplaceId: id });
    }
    return;
}

async function deletePosition(args) {
    await Position.findOneAndRemove(args);
    return;
}

async function deleteWorkplacePosition(args) {
    await Position.findOneAndRemove(args);
    return;
}

async function createWorkplace(args) {
    let lastID = 0;
    await Workplace.findOne().sort({ _id: -1 }).limit(1).then(result => {
        lastID = result.toJSON()._id;
    }).catch(err => {
        lastID = 0;
    });

    let employeesArray = [];

    if (args.employees) {
        if (args.employees.length > 1) {
            let employees = args.employees;
            for (let employee of employees) {
                await axios.post('http://employees:80/api/employees', employee).then(response => {
                    employeesArray.push(response.data.id);
                }).catch(error => {
                });
            }
        } else {
            await axios.post('http://employees:80/api/employees', args.employees).then(response => {
                employeesArray.push(response.data.id);
            }).catch(error => {
            });
        }
    }

    let newWorkplace = new Workplace({
        _id: lastID + 1,
        companyName: args.companyName,
        description: args.description,
        industry: args.industry,
        website: args.website,
        specialities: args.specialities,
        refPositions: `/workplaces/${lastID + 1}/positions`,
        employees: employeesArray
    });

    let workplace = await newWorkplace.save();

    return {
        _id: workplace._id,
        companyName: workplace.companyName,
        description: workplace.description,
        industry: workplace.industry,
        website: workplace.website,
        specialities: workplace.specialities,
        employees: workplace.employees
    };
}

async function createPosition(args) {
    let lastID = 0;
    await Position.findOne({ workplaceId: args.workplaceId }).sort({ _id: -1 }).limit(1).then(result => {
        lastID = result.toJSON().id;
    }).catch(err => {
        lastID = 0;
    });

    let overallID = 0;
    await Position.findOne().sort({ _id: -1 }).limit(1).then(result => {
        overallID = result.toJSON()._id;
    }).catch(err => {
        overallID = 0;
    });

    let newPosition = new Position({
        _id: overallID + 1,
        id: lastID + 1,
        workplaceId: args.workplaceId,
        positionName: args.positionName,
        location: args.location,
        workTimeNorm: args.workTimeNorm,
        description: args.description,
        requirements: args.requirements,
        salary: args.salary
    });

    let position = await newPosition.save();

    return {
        id: position.id,
        workplaceId: position.workplaceId,
        positionName: position.positionName,
        location: position.location,
        workTimeNorm: position.workTimeNorm,
        description: position.description,
        requirements: position.requirements,
        salary: position.salary
    };
}

async function editWorkplace(args) {
    let specialities = [];
    let employeesArray = [];

    await Workplace.findOne({ _id: args._id }).then(result => {
        specialities = result.specialities;
        employeesArray = result.employees;
    }).catch(err => {
    });

    if (args.specialities != null) {
        specialities = specialities.concat(args.specialities);
    }

    if (args.employees) {
        if (args.employees.length > 1) {
            let employees = args.employees;
            for (let employee of employees) {
                await axios.post('http://employees:80/api/employees', employee).then(response => {
                    employeesArray.push(response.data.id);
                }).catch(error => {
                });
            }
        } else {
            await axios.post('http://employees:80/api/employees', args.employees).then(response => {
                employeesArray.push(response.data.id);
            }).catch(error => {
            });
        }
    }

    let newWorkplace = new Workplace({
        _id: args._id,
        companyName: args.companyName,
        description: args.description,
        industry: args.industry,
        website: args.website,
        specialities: specialities,
        employees: employeesArray
    });

    let workplace = await Workplace.findOneAndUpdate({ _id: args._id }, newWorkplace, { new: true });

    return {
        _id: workplace._id,
        companyName: workplace.companyName,
        description: workplace.description,
        industry: workplace.industry,
        website: workplace.website,
        specialities: workplace.specialities,
        employees: workplace.employees
    };
}

async function editPosition(args) {
    let requirements = [];

    await Position.findOne({ _id: args._id }).then(result => {
        requirements = result.requirements;
    }).catch(err => {
    });

    if (args.requirements != null) {
        requirements = requirements.concat(args.requirements);
    }

    let newPposition = {
        positionName: args.positionName,
        location: args.location,
        workTimeNorm: args.workTimeNorm,
        description: args.description,
        requirements: requirements,
        salary: args.salary
    };

    let position = await Position.findOneAndUpdate({ _id: args._id }, newPposition, { new: true });

    return {
        _id: position._id,
        workplaceId: position.workplaceId,
        positionName: position.positionName,
        location: position.location,
        workTimeNorm: position.workTimeNorm,
        description: position.description,
        requirements: position.requirements,
        salary: position.salary
    };
}

async function editWorkplacePosition(args) {
    let requirements = [];

    await Position.findOne({ id: args.id, workplaceId: args.workplaceId }).then(result => {
        requirements = result.requirements;
    }).catch(err => {
    });

    if (args.requirements != null) {
        requirements = requirements.concat(args.requirements);
    }

    let newPposition = {
        positionName: args.positionName,
        location: args.location,
        workTimeNorm: args.workTimeNorm,
        description: args.description,
        requirements: requirements,
        salary: args.salary
    };

    let position = await Position.findOneAndUpdate({ id: args.id, workplaceId: args.workplaceId }, newPposition, { new: true });

    return {
        id: position.id,
        workplaceId: position.workplaceId,
        positionName: position.positionName,
        location: position.location,
        workTimeNorm: position.workTimeNorm,
        description: position.description,
        requirements: position.requirements,
        salary: position.salary
    };
}

let serviceObject = {
    WrokplaceService: {
        WorkplaceServicePort: {
            getWorkplace: getWorkplace,
            getWorkplaces: getWorkplaces,
            getPosition: getPosition,
            getPositions: getPositions,
            getWorkplacePositions: getWorkplacePositions,
            getWorkplacePosition: getWorkplacePosition,
            deleteWorkplace: deleteWorkplace,
            deletePosition: deletePosition,
            deleteWorkplacePosition: deleteWorkplacePosition,
            createWorkplace: createWorkplace,
            createPosition: createPosition,
            editWorkplace: editWorkplace,
            editPosition: editPosition,
            editWorkplacePosition: editWorkplacePosition
        }
    }
};

export default serviceObject;