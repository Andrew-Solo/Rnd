import ItemPanel from "../../components/panels/ItemPanel";

export default function GamePage () {
  const game = {name: "mrak", title: "Мрак", owner: "AndrewSolo", image: "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"};

  return (
    <ItemPanel {...game} subtitle={game.owner}>
      New component
    </ItemPanel>
  );
}