export interface IAgent {
    firstname: string;
    lastname: string;
    city: string;
    country: string;
    company: string;
    phone: string;
    address: string;
    email: string;
    integrationEmail: string;
    id: string;
    aggregateId: string;
    agentTypeForm: IAgentTypeForm
}

export interface IAgentTypeForm {
    typeFormUrl: string;
    spreadsheetUrl: string;
    type: string;
    aggregateId: string;
}