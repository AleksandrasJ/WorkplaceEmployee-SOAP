import express from 'express';
import bodyParser from 'body-parser';
import workplacesRoutes from './routes/workplaces.js';
import positionsRoutes from './routes/positions.js';
import allPositionsRoutes from './routes/allPositions.js';
// SOAP imports
import fs from 'fs';
import soap from 'soap';
import serviceObject from './tools/serviceObject.js';

const app = express();
const PORT = 80;

app.use(bodyParser.json());
app.use('/workplaces', workplacesRoutes);
app.use('/workplaces/:id/positions', (req, res, next) => {
    req.id = req.params.id;
    next();
}, positionsRoutes);
app.use('/positions', allPositionsRoutes);

const xml = fs.readFileSync("service.wsdl", "utf8");

const wsdl_path = "/wsdl";
soap.listen(app, wsdl_path, serviceObject, xml);
// SOAP END
app.get('/', (req, res) => res.send('<h1>REST API for workplaces and positions</h1>'));

app.listen(PORT, () => console.log(`Web service is listening on port ${PORT}`));