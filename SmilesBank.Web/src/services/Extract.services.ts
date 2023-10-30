import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IExtract } from "src/interfaces/IExtract";
import { ConfigService } from "./Config.Services";
import { IFilterInsertExtract } from "src/interfaces/IFilterInsertExtract";
import { ICancel } from "src/interfaces/ICancel";
import { catchError } from "rxjs";

@Injectable()
export class ExtractService {
    constructor (private readonly http: HttpClient, private readonly config: ConfigService) {}

    getExtract() {
        return this.http.get<IExtract>(`${this.config.getApi()}/ListExtract`)
    }

    insert(filter: IFilterInsertExtract) {
        return this.http.post<IFilterInsertExtract>(`${this.config.getApi()}/InsertExtract`, filter)
    }

    Cancel(id: string) {
        const body = {
            id: id
        }
        return this.http.post<ICancel>(`${this.config.getApi()}/Cancel`, body)
    }
}