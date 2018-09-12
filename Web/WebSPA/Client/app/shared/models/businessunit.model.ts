export interface IBusinessUnit {
    Name: string;
    LogoUri: string;
    BannerUri: string;
    Description: string;
    CeoName: string;
    CeoProfileImageUri: string;
    WebSite: string;
    Address: string;
    CampusArea: string;
    StartingDate: string;
    BusinessSector: string;
    Country: string;
    NumberOfEmployees: string;
    NetWorth: string;
    VideoSite: string;
    CareerSite: string;
    Email: string;
    Phone: string;
}

export interface IBusinessUnitQuestion {
    Id: string;
    Statement: string;
    AnswerType: string;
    AllowedAnswers: string;
}