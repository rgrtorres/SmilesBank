import { Component, OnInit } from '@angular/core';
import { IExtract } from 'src/interfaces/IExtract';
import { IFilterExtract } from 'src/interfaces/IFilterExtract';
import { IFilterInsertExtract } from 'src/interfaces/IFilterInsertExtract';
import { IUpdateExtract } from 'src/interfaces/IUpdateExtract';
import { ExtractService } from 'src/services/Extract.services';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = 'SmilesBank.Web';

    edit: boolean = false

    lstExtract: Array<IExtract> = []
    totalExtract: number = 0

    filterInsert: IFilterInsertExtract = {
        description: "",
        amount: 0,
        status: "Válido",
        type: true
    }

    filterUpdate: IUpdateExtract = {
        id: "",
        description: "",
        amount: 0,
    }

    filterDate: IFilterExtract = {
        startDate: new Date(),
        endDate: new Date()
    }
    
    InputDescription: string = ""
    InputAmount: number = 0
    InputStatus: string = ""
    InputId: string = ""

    constructor(private readonly extractService: ExtractService) {}

    ngOnInit(): void {
        this.extracts()
    }

    extracts() {
        return this.extractService.getExtract().subscribe(
            (data) => {
                this.lstExtract = data as unknown as Array<IExtract>
            }
        )
    }

    convertDate(){
        const date = new Date()
        const year = String(date.getFullYear)
        const month = String(date.getMonth() + 1).padStart(2, '0')
        const day = String(date.getDate()).padStart(2, '0');

        const formatted = `${year}-${month}-${day}`

        return formatted
    }


    extractsByDate(filter: IFilterExtract) {
        console.log(filter)
        return this.extractService.getExtractByDate(filter).subscribe(
            (data) => {
                this.lstExtract = data as unknown as Array<IExtract>
                console.log(data)
            }
        )
    }

    insertExtract() {
        this.filterInsert.amount = this.InputAmount
        this.filterInsert.description = this.InputDescription

        this.InputAmount = 0
        this.InputDescription = ""
        this.InputId = ""

        return this.extractService.insert(this.filterInsert).subscribe(() => this.extracts())
    }

    clickUpdate(id: string, desc: string, amount: number) {
        this.edit = true
        this.InputId = id
        this.InputDescription = desc
        this.InputAmount = amount
    }

    cancelExtract(id: string) {
        return this.extractService.Cancel(id).subscribe(() => this.extracts())
    }

    clickCancelEdit() {
        this.edit = false
        this.InputAmount = 0
        this.InputDescription = ""
        this.InputId = ""
    }

    updateExtract() {
        this.filterUpdate.amount = this.InputAmount
        this.filterUpdate.description = this.InputDescription
        this.filterUpdate.id = this.InputId

        this.extractService.update(this.filterUpdate).subscribe(() => this.extracts())

        this.clickCancelEdit()
    }

    automatic: IExtract = {
        id: "",
        description: "",
        date: new Date(),
        amount: 0,
        type: false,
        status: "",
    }

    auto(){
        return this.extractService.automatic().subscribe(() => this.extracts())
    }

    getTotal() {
        return this.lstExtract.reduce((total, extract) => {
            return total + extract.amount
        }, 0)
    }
}
