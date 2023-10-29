import { Component, OnInit } from '@angular/core';
import { ExtractService } from 'services/Extract.services';
import { IExtract } from 'src/interfaces/IExtract';
import { IFilterExtract } from 'src/interfaces/IFilterExtract';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = 'SmilesBank.Web';

    lstExtract: Array<IExtract> = []

    constructor(private readonly extractService: ExtractService) {}

    ngOnInit(): void {
        
    }

    extracts() {
        return this.extractService.getExtract().subscribe(
            data => {
                console.log(data)
            }
        )
    }
}
