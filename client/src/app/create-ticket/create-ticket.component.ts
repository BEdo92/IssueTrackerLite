import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { TicketService } from '../_services/ticket.service';

@Component({
  selector: 'app-create-ticket',
  imports: [ReactiveFormsModule],
  templateUrl: './create-ticket.component.html',
  styleUrl: './create-ticket.component.css'
})
export class CreateTicketComponent {
  private ticketService = inject(TicketService);  
  ticketForm = new FormGroup({
    title: new FormControl('', { nonNullable: true }),
    description: new FormControl('', { nonNullable: true }),
    priority: new FormControl('Medium', { nonNullable: true }),
  });

  onSubmit() {
    if (this.ticketForm.valid) {
      console.log('Creating ticket:', this.ticketForm.value);
      this.ticketService.createTicket(this.ticketForm.value).subscribe(response => {
        console.log('Ticket created:', response);
      });
      this.ticketForm.reset({ priority: 'Medium' });
    }
  }
}
