import { RedeSocial } from "./RedeSocial";
import { Evento } from "./Evento";

export interface Orador {
    id: number;
    nome: string;
    miniCurriculo: string;
    imagemURL: string;
    telefone: string;
    email: string;
    redesSociais: RedeSocial[];
    oradoresEventos: Evento[];
}
