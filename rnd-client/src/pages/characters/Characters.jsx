export default function Characters () {
  return (
    <div>
      New component
    </div>
  );
}

const charactersData = [
  new Character("Мрак", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"),
  new Character("Сказки Латаифа", "Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971208950648862/avatar.jpg"),
];

function Character(title, owner, image) {
  return {title, owner, image};
}