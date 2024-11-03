import { ResolveFn, Routes } from '@angular/router';
import { ListagemContatosComponent } from './listar/listagem-contatos.component';
import { inject } from '@angular/core';
import { ContatoService } from './services/contato.service';
import { ListarContatoViewModel } from './models/contato.models';

const listagemContatosResolver: ResolveFn<ListarContatoViewModel[]> = () => {
  return inject(ContatoService).selecionarTodos();
};

export const contatosRoutes: Routes = [
  { path: '', redirectTo: 'listar', pathMatch: 'full' },
  {
    path: 'listar',
    component: ListagemContatosComponent,
    resolve: {
      contatos: listagemContatosResolver,
    },
  },
];
