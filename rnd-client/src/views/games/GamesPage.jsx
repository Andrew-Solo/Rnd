import CardsGrid from "../../components/ui/cards/CardsGrid";
import DataContainer from "../../components/containers/data/DataContainer";

export default function GamesPage () {
  return (
    <DataContainer title="Игры">
      <CardsGrid data={gamesData.map(game => ({...game, subtitle: game.owner}))}/>
    </DataContainer>
  )
}

const gamesData = [
  new Game("mrak", "Мрак", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"),
  new Game("lataif", "Сказки Латаифа", "Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971208950648862/avatar.jpg"),
];

function Game(name, title, owner, image) {
  return {name, title, owner, image};
}