import HeroGrid from "../../components/HeroGrid";

export default function Games () {
  return (
    <HeroGrid data={gamesData.map(game => ({image: game.image, title: game.title, subtitle: game.owner}))}/>
  )
}

const gamesData = [
  new Game("Мрак", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"),
  new Game("Сказки Латаифа", "Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971208950648862/avatar.jpg"),
];

function Game(title, owner, image) {
  return {title, owner, image};
}