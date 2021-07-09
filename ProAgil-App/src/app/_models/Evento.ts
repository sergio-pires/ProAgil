import { Lote } from "./Lote";
import { Orador } from "./Orador";
import { RedeSocial } from "./RedeSocial";

export interface Evento {
    id: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number; 
    imagemURL: string; 
    telefone: string; 
    email: string; 
    lotes: Lote[];
    redesSociais: RedeSocial[];
    oradoresEventos: Orador[];
}
