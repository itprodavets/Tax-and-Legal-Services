import CbcOecdComponent from "@/modules/cbc/CbcOecd.vue";
import CbcListComponent from "@/modules/cbc/views/CbcList.vue";
import CbcCreateComponent from "@/modules/cbc/views/CbcCreate.vue";
import CbcDetailComponent from "@/modules/cbc/views/CbcDetail.vue";

export const cbcRoutes = [
  {
    path: "/cbc-report",
    name: "cbc-report",
    component: CbcOecdComponent,
    redirect: "/cbc-report/list",
    children: [
      {
        path: "list",
        name: "list",
        component: CbcListComponent
      },
      {
        path: "",
        redirect: "/cbc-report/list"
      },
      {
        path: "create",
        component: CbcCreateComponent
      },
      {
        path: ":id",
        component: CbcDetailComponent
      }
    ]
  }
];
