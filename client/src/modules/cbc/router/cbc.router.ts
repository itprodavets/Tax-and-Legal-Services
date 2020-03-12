import ReportDataView from "@/modules/cbc/ReportData.vue";
import ReportView from "@/modules/cbc/views/report/Report.vue";
import ReportDetailView from "@/modules/cbc/views/report/ReportDetail.vue";
import ReportListView from "@/modules/cbc/views/report/ReportList.vue";
import ReportDataDetailView from "@/modules/cbc/views/ReportDataDetail.vue";
import ReportDataListView from "@/modules/cbc/views/ReportDataList.vue";
import ReportDataMessageView from "@/modules/cbc/views/ReportDataMessage.vue";
import AdditionalInformationDetailView from "@/modules/cbc/views/steps/additional-info/AdditionalInformationDetail.vue";
import AdditionalInformationListView from "@/modules/cbc/views/steps/additional-info/AdditionalInformationList.vue";
import AdditionalInformationStepView from "@/modules/cbc/views/steps/additional-info/AdditionalInformationStep.vue";
import ConstituentEntityDetailView from "@/modules/cbc/views/steps/constituent-entity/ConstituentEntityDetail.vue";
import ConstituentEntityListView from "@/modules/cbc/views/steps/constituent-entity/ConstituentEntityList.vue";
import ConstituentEntityStepView from "@/modules/cbc/views/steps/constituent-entity/ConstituentEntityStep.vue";
import ReportBodyDetailView from "@/modules/cbc/views/steps/report-body/ReportBodyDetail.vue";
import ReportBodyListView from "@/modules/cbc/views/steps/report-body/ReportBodyList.vue";
import ReportBodyStepView from "@/modules/cbc/views/steps/report-body/ReportBodyStep.vue";
import ReportingEntityStepView from "@/modules/cbc/views/steps/ReportingEntityStep.vue";

export const cbcRoutes = [
  {
    path: "/cbc-report",
    name: "cbc.report",
    component: ReportDataView,
    redirect: "/cbc-report/list",
    children: [
      {
        path: "list",
        name: "list",
        component: ReportDataListView
      },
      {
        path: ":id",
        name: "cbc.report.detail",
        redirect: ":id/report/list",
        component: ReportDataDetailView,
        children: [
          {
            path: "report",
            name: "report",
            component: ReportView,
            children: [
              {
                path: "list",
                name: "report.list",
                component: ReportListView
              },
              {
                path: ":reportId",
                name: "report.detail",
                redirect: ":reportId/constituent-entity",
                component: ReportDetailView,
                children: [
                  {
                    path: "constituent-entity",
                    name: "constituent.entity",
                    redirect: "constituent-entity/list",
                    component: ConstituentEntityStepView,
                    children: [
                      {
                        path: "list",
                        name: "constituent.entity.list",
                        component: ConstituentEntityListView
                      },
                      {
                        path: ":constituentEntityId",
                        name: "constituent.entity.detail",
                        component: ConstituentEntityDetailView
                      }
                    ]
                  },
                  {
                    path: "reporting-entity",
                    name: "reporting.entity",
                    component: ReportingEntityStepView
                  },
                  {
                    path: "additional-information",
                    name: "additional.information",
                    redirect: "additional-information/list",
                    component: AdditionalInformationStepView,
                    children: [
                      {
                        path: "list",
                        name: "additional.information.list",
                        component: AdditionalInformationListView
                      },
                      {
                        path: ":additionalInfoId",
                        name: "additional.information.detail",
                        component: AdditionalInformationDetailView
                      }
                    ]
                  },
                  {
                    path: "report-body",
                    name: "report.body",
                    redirect: "report-body/list",
                    component: ReportBodyStepView,
                    children: [
                      {
                        path: "list",
                        name: "report.body.list",
                        component: ReportBodyListView
                      },
                      {
                        path: ":reportBodyId",
                        name: "report.body.detail",
                        component: ReportBodyDetailView
                      }
                    ]
                  }
                ]
              }
            ]
          },
          {
            path: "message",
            name: "message",
            component: ReportDataMessageView
          }
        ]
      }
    ]
  }
];
