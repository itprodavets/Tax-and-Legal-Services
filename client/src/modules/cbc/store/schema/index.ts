import {schema} from "normalizr";

const docSpecSchema: schema.Entity = new schema.Entity("docSpec");

const reportingEntitySchema: schema.Entity = new schema.Entity("reportingEntity");

const summarySchema: schema.Entity = new schema.Entity("summary");
const constEntitiesSchema: schema.Entity = new schema.Entity("constEntities");
const cbcReportsSchema: schema.Entity = new schema.Entity("cbcReports", {
    docSpec: docSpecSchema,
    summary: summarySchema,
    constEntities: [constEntitiesSchema]
});

const otherInfoSchema: schema.Entity = new schema.Entity("otherInfo");
const additionalInfoSchema: schema.Entity = new schema.Entity("additionalInfo", {
    docSpec: docSpecSchema,
    otherInfo: [otherInfoSchema]
});

const messageSpecSchema: schema.Entity = new schema.Entity("messageSpec");
const cbcBodySchema: schema.Entity = new schema.Entity("cbcBody", {
  reportingEntity: reportingEntitySchema,
  cbcReports: [cbcReportsSchema],
  additionalInfo: [additionalInfoSchema]
});

const reportDataSchema: schema.Entity = new schema.Entity("reportData", {
  messageSpec: messageSpecSchema,
  cbcBody: [cbcBodySchema]
});
