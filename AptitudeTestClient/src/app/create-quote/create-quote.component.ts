import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { QuotesDataClient } from '../quotes-data-client';
import { Quote } from '../models/quote';
import { JsonPipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { NO_ERRORS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-create-quote',
  imports: [ReactiveFormsModule, JsonPipe, CommonModule],
  templateUrl: './create-quote.component.html',
  styleUrl: './create-quote.component.css',
  schemas: [NO_ERRORS_SCHEMA],
})
export class CreateQuoteComponent {
  // This will give you access to the Quotes controller endpoints
  private readonly quotesDataClient = inject(QuotesDataClient);
  states: any[] = [];
  // What can you do with a list of states with the form?
  ngOnInit(): void {
    // Subscribing to the Observable returned by getAllStates()
    this.quotesDataClient.getAllStates().subscribe((data) => {
      // Success: Store the data (list of states) when it arrives
      this.states = data;
    });
  }

  // Here is a sample of a form you can use
  form = new FormGroup({
    name: new FormControl(''),
    tiv: new FormControl(0),
    stateId: new FormControl(0),
  });

  /**
   * This will call the /quotes POST endpoint
   */
  submit(): void {
    this.quotesDataClient
      .createQuote(this.form.value as Quote)
      .subscribe((retVal) => {
        if (retVal) {
          window.location.href = '/quotes';
        } else {
        }
      });
  }
}
