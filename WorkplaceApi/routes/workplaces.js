import express, { response } from 'express';
import { Position, Workplace } from '../tools/database.js';
import errorTemplate from '../tools/error.js';
import axios from 'axios';

const router = express.Router();

router.get('/', async (req, res) => {
    await Workplace.find().then(async results => {
        if (results.length > 0) {
            let resultsArray = [];

            for (let result of results) {
                let ids = [];
                let employeesArray = [];

                result = result.toJSON();

                ids = result.employees;
                result.employees = [];

                delete result.__v;

                for (let id of ids) {
                    await axios.get(`http://employees:80/api/employees/${id}`).then(response => {
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
                result.employees = employeesArray;
                resultsArray.push(result);
            }
            res.status(200);
            res.send(resultsArray);
        } else {
            res.status(404);
            res.send(errorTemplate(404, 'No workplaces found!'));
        }
    }).catch(err => {
        res.status(500);
        res.send(errorTemplate(500, 'Failed to collect data!'));
    });
});

router.post('/', async (req, res) => {
    let lastID = 0;
    await Workplace.findOne().sort({ _id: -1 }).limit(1).then(result => {
        lastID = result.toJSON()._id;
    }).catch(err => {
        lastID = 0;
    });

    let workplace = new Workplace({
        _id: lastID + 1,
        companyName: req.body.companyName || "",
        description: req.body.description || "",
        industry: req.body.industry || "",
        website: req.body.website || "",
        specialities: req.body.specialities || [],
        refPositions: `/workplaces/${lastID + 1}/positions`
    });

    let employeesArray = [];
    let success = true;
    let statusCode;
    let message;
    if (req.body.employees && req.body.companyName) {
        let temp = req.body.employees;
        for (let employee of temp) {
            await axios.post('http://employees:80/api/employees', employee).then(response => {
                employeesArray.push(response.data.id);
            }).catch(error => {
                statusCode = error.response.data.status;
                message = error.response.data.title;
                success = false;
            });
        }
    }

    workplace.employees = employeesArray;

    if (success) {
        if (!workplace.validateSync()) {
            await workplace.save().then(result => {
                res.status(201);
                res.location(`workplaces/${workplace._id}`);
                res.send(result);
            }).catch(err => {
                res.status(500);
                res.send(errorTemplate(500, 'Failed to collect data!'));
            });
        } else {
            res.status(400);
            res.send(errorTemplate(400, 'To create resource compnayName is needed!'));
        }
    } else {
        res.status(statusCode);
        res.send(errorTemplate(statusCode, message));
    }

});

router.get('/:id', async (req, res) => {
    await Workplace.findOne({ _id: req.params.id }).then(async result => {
        if (result !== null) {
            let ids = [];
            let employeesArray = [];

            result = result.toJSON();

            ids = result.employees;
            result.employees = [];

            delete result.__v;

            for (let id of ids) {
                await axios.get(`http://employees:80/api/employees/${id}`).then(response => {
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

            result.employees = employeesArray;
            res.status(200);
            res.send(result);
        } else {
            res.status(404);
            res.send(errorTemplate(404, 'No workplaces found!'));
        }
    }).catch(err => {
        res.status(500);
        res.send(errorTemplate(500, 'Failed to collect data!'));
    });
});

router.put('/:id', async (req, res) => {
    let specialities = [];
    let employeesArray = [];

    await Workplace.findOne({ _id: req.params.id }).then(result => {
        employeesArray = result.employees;
        specialities = result.specialities;
    }).catch(err => {
    });

    if (req.body.specialities != null) {
        specialities = specialities.concat(req.body.specialities);
    }

    let workplace = new Workplace({
        _id: req.params.id,
        companyName: req.body.companyName,
        description: req.body.description,
        industry: req.body.industry,
        website: req.body.website,
        specialities: specialities
    });

    let statusCode;
    let message;
    let success = true;
    if (req.body.employees) {
        let temp = req.body.employees;
        for (let employee of temp) {
            await axios.post('http://employees:80/api/employees', employee).then(response => {
                employeesArray.push(response.data.id);
            }).catch(error => {
                statusCode = error.response.data.status;
                message = error.response.data.title;
                success = false;
            });
        }
    }

    workplace.employees = employeesArray;

    if (success) {
        await Workplace.findOneAndUpdate({ _id: req.params.id }, workplace, { new: true }).then(result => {
            if (result !== null) {
                res.status(200);
                res.location(`/workplaces/${req.params.id}`);
                res.send(result);
            } else {
                res.status(404);
                res.send(errorTemplate(404, 'No workplaces found for updation!'));
            }
        }).catch(err => {
            res.status(500);
            res.send(errorTemplate(500, 'Failed to collect data!'));
        });
    } else {
        res.status(statusCode);
        res.send(errorTemplate(statusCode, message));
    }
});

router.delete('/:id', async (req, res) => {
    let length = 0;
    await Workplace.findOneAndRemove({ _id: req.params.id }).then(result => {
        if (result) {
            let temp = result.employees;
            for (let employee of temp) {
                axios.delete(`http://employees:80/api/employees/${employee}`).then(response => { }).catch(error => { });
            }
            res.status(204);
            res.send('');
        } else {
            res.status(404);
            res.send(errorTemplate(404, 'No workplaces found for deletion!'));
        }
    }).catch(err => {
        res.status(500);
        res.send(errorTemplate(500, 'Failed to collect data!'));
    });
    await Position.find({ workplaceId: req.params.id }).then(result => {
        length = result.length;
    });

    for (let i = 0; i < length; ++i) {
        await Position.findOneAndRemove({ workplaceId: req.params.id });
    }
});

router.all('/', (req, res) => {
    res.status(405);
    res.send(errorTemplate(405, 'Method not allowed!'));
});

router.all('/:id', (req, res) => {
    res.status(405);
    res.send(errorTemplate(405, 'Method not allowed!'));
});

export default router;