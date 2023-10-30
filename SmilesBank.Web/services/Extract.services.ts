import { Injectable } from "@angular/core";
import { IConfig } from "src/interfaces/IConfig";
import { HttpClient } from "@angular/common/http";
import { IExtract } from "src/interfaces/IExtract";
import { IFilterExtract } from "src/interfaces/IFilterExtract";

@Injectable()
export class ExtractService {
    private config: IConfig = {
        apiURL: ""
    }

    constructor (private readonly http: HttpClient) {
        this.http.get<IConfig>('../assets/data/appConfig.json').subscribe(
            (data) => {
                this.config = data as IConfig
            }
        )

        console.log('API: ', this.config)
    }

    getExtract() {
        return this.http.get<IExtract>(`${this.config.apiURL}/`);
    }
}