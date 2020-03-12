import {Guid} from "@/core/common/guid";
import {MonAmnt, Report, ReportBody, ReportBodyCreateRequest, ReportBodyRequest, ReportBodyUpdateRequest, Summary} from "@/modules/cbc/models";
import {CountryEnum} from "@/modules/country/models";
import {CurrencyEnum} from "@/modules/currency/models";
import _ from "lodash";
import {ActionContext, ActionTree} from "vuex";
import {ReportState} from '../report.state';
import {ReportBodyState} from "./report-body.state";

export const actions: ActionTree<ReportBodyState, ReportState> = {
	list: async (
		action: ActionContext<ReportBodyState, ReportState>,
		request: ReportBodyRequest
	) => {
		let data = action.rootGetters["cbc/report/report"] as Report;
		
		const reportCodes = _.chain(data.reports).map(x => x.jurisdiction).uniq().value();
		const countryEnums = _.flatten(data.constituentEntities.map(x => x.organisation.jurisdictions)) as CountryEnum[];
		const codes = _.chain(countryEnums.filter(x => x && !reportCodes.some(y => y === x))).uniq().value();
		
		if (data && data.id === request.reportId) {
			action.commit("GET_REPORT_BODY_LIST", createReport(data.reports, codes));
		} else {
			data = _.find(action.rootGetters["cbc/report/reports"], (x: Report) => x.id === request.reportId);
			if (data)
				action.commit("GET_REPORT_BODY_LIST", createReport(data.reports, codes));
			else
				action.commit("GET_REPORT_BODY_LIST", createReport([], codes));
		}
	},
	get: async (
		action: ActionContext<ReportBodyState, ReportState>,
		id: string | number
	) => {
		action.commit("GET_REPORT_BODY", id);
	},
	create: async (
		action: ActionContext<ReportBodyState, ReportState>,
		request: ReportBodyCreateRequest
	) => {
		request.reportBody.id = Guid.create().toString();
		action.commit("CREATE_REPORT_BODY", request.reportBody);
		return request.reportBody.id;
	},
	update: async (
		action: ActionContext<ReportBodyState, ReportState>,
		request: ReportBodyUpdateRequest
	) => {
		action.commit("UPDATE_REPORT_BODY", request.reportBody);
	}
};

function createReport(reports: ReportBody[] = [], codes: CountryEnum[]): Array<ReportBody> {
	if (codes && codes.length > 0)
		codes.forEach(code => reports.push({
			id: Guid.create().toString(),
			jurisdiction: code,
			summary: createSummary(),
			doc: {}
		} as ReportBody));
	return reports;
}

function createSummary(): Summary {
	return {
		id: Guid.create().toString(),
		unrelated: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		related: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		total: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		profitOrLoss: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		taxPaid: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		taxAccrued: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		capital: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		earnings: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt,
		nbEmployees: "0",
		assets: {
			currency: CurrencyEnum.USD,
			value: "0"
		} as MonAmnt
	} as Summary;
}