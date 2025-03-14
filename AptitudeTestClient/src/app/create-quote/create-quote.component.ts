import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';
import {QuotesDataClient} from '../quotes-data-client';
import {Quote} from '../models/quote';
import {JsonPipe} from '@angular/common';

@Component({
  selector: 'app-create-quote',
  imports: [
    ReactiveFormsModule,
    JsonPipe
  ],
  templateUrl: './create-quote.component.html',
  styleUrl: './create-quote.component.css'
})
export class CreateQuoteComponent {
  // This will give you access to the Quotes controller endpoints
  private readonly quotesDataClient = inject(QuotesDataClient);

  // What can you do with a list of states with the form?
  // this.quotesDataClient.getAllStates().subscribe(s => console.log(s));

  // Here is a sample of a form you can use
  form = new FormGroup({
    name: new FormControl(''),
    tiv: new FormControl(0),
    stateId: new FormControl(0)
  });

  /**
   * This will call the /quotes POST endpoint
   */
  submit(): void {
    this.quotesDataClient.createQuote(this.form.value as Quote)
      .subscribe((retVal) => {
        // Implement any logic you see fit here.
      });
  }
}
