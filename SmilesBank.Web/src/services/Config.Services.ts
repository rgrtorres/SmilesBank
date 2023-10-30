import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { IConfig } from "src/interfaces/IConfig";
import { IExtract } from "src/interfaces/IExtract";

@Injectable()
export class ConfigService {
    public appConfig?: IConfig;

    constructor(private readonly http: HttpClient) { }

    public loadAppConfig(): Observable<IConfig> {
        return this.http.get('../assets/data/appConfig.json')
            .pipe(
                map(
                    data => {
                        return this.appConfig = data as IConfig
                    }
                )
            )
    }

    public getApi() {
        return this.appConfig?.apiURL
    }
}