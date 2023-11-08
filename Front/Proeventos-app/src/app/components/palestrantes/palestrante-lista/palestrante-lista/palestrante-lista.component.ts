import {Component} from '@angular/core';
import {Speaker} from "../../../../models/Speaker";
import {ToastrService} from "ngx-toastr";
import {SpeakerService} from "../../../../services/speaker/speaker.service";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
    selector: 'app-palestrante-lista',
    templateUrl: './palestrante-lista.component.html',
    styleUrls: ['./palestrante-lista.component.scss']
})
export class PalestranteListaComponent {
    speakerFilter: any;
    filteredSpeakers: any;
    speakers: Speaker[];

    public ngOnInit(): void {
        this.spinner.show();

        setTimeout(() => {
            this.spinner.hide();
        }, 500);

        this.loadSpeakers();
    }

    constructor(private toastrService: ToastrService, private service: SpeakerService, private spinner: NgxSpinnerService) {
        this.speakers = [];
    }

     set listFilter(value: string) {
        this.speakerFilter = value;
        value = value.toLowerCase();
        this.filteredSpeakers = this.speakerFilter ? this.speakers.filter((s: Speaker) => s.name.toLowerCase().includes(value) || s.email.toLowerCase().includes(value)) : this.speakers;
    }


    public loadSpeakers(): void {
        this.service.getAll().subscribe({
                next: (speakers: Speaker[]) => {
                    console.log(speakers);
                    this.speakers = speakers;
                    this.filteredSpeakers = speakers;
                },
                error: (error: any) => {
                    console.log(error)
                    this.toastrService.error("Error while trying to load the events", "ERROR")
                },
                complete: () => {
                }
            }
        ).add(() => this.spinner.hide())
    }
}
