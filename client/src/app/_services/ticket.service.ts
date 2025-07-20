import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5000/api/';
  currentUser = signal<User | null>(null);

  getTickets() {
    return this.http.get<any[]>(this.baseUrl + 'tickets');
  }

  createTicket(ticket: any) {
    console.log("Creating ticket:", ticket);
    return this.http.post<any>(this.baseUrl + 'tickets', ticket);
  }

  updateTicket(ticketId: number, ticket: any) {
    return this.http.put<any>(`${this.baseUrl}tickets/${ticketId}`, ticket);
  }

  deleteTicket(ticketId: number) {
    return this.http.delete(`${this.baseUrl}tickets/${ticketId}`);
  }
}