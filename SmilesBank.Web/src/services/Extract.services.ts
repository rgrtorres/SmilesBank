import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IExtract } from "src/interfaces/IExtract";
import { ConfigService } from "./Config.Services";
import { IFilterInsertExtract } from "src/interfaces/IFilterInsertExtract";
import { ICancel } from "src/interfaces/ICancel";
import { catchError } from "rxjs";
import { IUpdateExtract } from "src/interfaces/IUpdateExtract";
import { IFilterExtract } from "src/interfaces/IFilterExtract";

@Injectable()
export class ExtractService {
    constructor (private readonly http: HttpClient, private readonly config: ConfigService) {}

    getExtract(filter: IFilterExtract) {
        return this.http.post<IExtract>(`${this.config.getApi()}/ListExtract`, filter)
    }

    insert(filter: IFilterInsertExtract) {
        return this.http.post<IFilterInsertExtract>(`${this.config.getApi()}/InsertExtract`, filter)
    }

    update(filter: IUpdateExtract) {
        return this.http.post<IUpdateExtract>(`${this.config.getApi()}/Update`, filter)
    }

    Cancel(id: string) {
        const body = {
            id: id
        }
        return this.http.post<ICancel>(`${this.config.getApi()}/Cancel`, body)
    }

    automatic() {
        return this.http.get(`${this.config.getApi()}/automatic`)
    }
}