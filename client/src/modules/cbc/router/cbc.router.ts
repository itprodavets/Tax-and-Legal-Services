import ReportDataView from "@/modules/cbc/ReportData.vue";
import ReportDataListView from "@/modules/cbc/views/ReportDataList.vue";
import ReportDataDetailView from "@/modules/cbc/views/ReportDataDetail.vue";

import ReportView from "@/modules/cbc/views/report/Report.vue";
import ReportDetailView from "@/modules/cbc/views/report/ReportDetail.vue";
import ReportListView from "@/modules/cbc/views/report/ReportList.vue";

import ConstituentEntityStepView from "@/modules/cbc/views/steps/constituent-entity/ConstituentEntityStep.vue";
import ReportingEntityStepView from "@/modules/cbc/views/steps/ReportingEntityStep.vue";
import AdditionalInformationStepView from "@/modules/cbc/views/steps/additional-info/AdditionalInformationStep.vue";
import ReportBodyStepView from "@/modules/cbc/views/steps/report-body/ReportBodyStep.vue";

import ReportDataMessageView from "@/modules/cbc/views/ReportDataMessage.vue";


export const cbcRoutes = [
  {
    path: "/cbc-report",
    name: "cbc-report",
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
                    component: ConstituentEntityStepView
                  },
                  {
                    path: "reporting-entity",
                    name: "reporting.entity",
                    component: ReportingEntityStepView
                  },
                  {
                    path: "additional-information",
                    name: "additional.information",
                    component: AdditionalInformationStepView
                  },
                  {
                    path: "report-body",
                    name: "report.body",
                    component: ReportBodyStepView
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
