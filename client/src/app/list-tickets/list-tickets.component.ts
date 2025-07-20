import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketService } from '../_services/ticket.service';

@Component({
  selector: 'app-list-tickets',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './list-tickets.component.html',
  styleUrl: './list-tickets.component.css'
})
export class ListTicketsComponent implements OnInit {
  tickets: any[] = [];
  loading = false;
  error: string | null = null;
  ticketService = inject(TicketService);
  showModal = false;
  selectedTicket: any = null;

  ngOnInit(): void {
    this.fetchTickets();
  }

  fetchTickets(): void {
    this.loading = true;
    this.ticketService.getTickets().subscribe({
      next: (tickets) => {
        this.tickets = tickets;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load tickets';
        this.loading = false;
      }
    });
  }

  openModal(ticket: any) {
    this.selectedTicket = ticket;
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
    this.selectedTicket = null;
  }
}
