import { Component, inject, OnInit } from '@angular/core';
import { QuotesDataClient } from '../quotes-data-client';
import { Quote } from '../models/quote';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { filter, map, switchMap } from 'rxjs';
import { JsonPipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { NO_ERRORS_SCHEMA } from '@angular/core';

@Component({
  selector: 'app-edit-quote',
  imports: [ReactiveFormsModule, JsonPipe, CommonModule],
  templateUrl: './edit-quote.component.html',
  styleUrl: './edit-quote.component.css',
  schemas: [NO_ERRORS_SCHEMA],
})
export class EditQuoteComponent implements OnInit {
  private readonly quotesDataClient = inject(QuotesDataClient);
  private readonly route = inject(ActivatedRoute);
  private quoteId$ = inject(ActivatedRoute).paramMap.pipe(
    map((params) => params.get('id'))
  );

  quoteId: string | null = null;

  form = new FormGroup({
    name: new FormControl(''),
    tiv: new FormControl(0),
    stateId: new FormControl(0),
  });

  states: any[] = [];

  ngOnInit() {
    // This is getting the id from the URL, calling the API to retrieve the quote
    // and then updating the form with the quotes values.
    this.quoteId$
      .pipe(
        filter((id) => id !== null),
        switchMap((id) => this.quotesDataClient.getQuoteById(id))
      )
      .subscribe((quote) => {
        this.quoteId = quote.id.toString();
        this.form.patchValue({
          name: quote.name,
          tiv: quote.tiv,
          stateId: quote.stateId,
        });
      });

    // Subscribing to the Observable returned by getAllStates()
    this.quotesDataClient.getAllStates().subscribe((data) => {
      // Success: Store the data (list of states) when it arrives
      this.states = data;
    });
  }

  /**
   * This will call the /quotes/{id} PUT endpoint
   * What changes do you need to make here for it to work?
   */
  submit(): void {
    if (this.quoteId !== null) {
      this.quotesDataClient
        .updateQuote(this.quoteId, this.form.value as Quote)
        .subscribe((retVal: any) => {
          if (retVal) {
            window.location.href = '/quotes';
          } else {
          }
        });
    }
  }
}
