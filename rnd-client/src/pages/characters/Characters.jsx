import HeroGrid from "../../components/HeroGrid";

export default function Characters () {
  return (
    <HeroGrid data={charactersData.map(character => ({image: character.image, title: character.title, subtitle: character.member}))}/>
  )
}

const charactersData = [
  new Character("Дакуродо", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1114260412926533763/f5450d3932dab6af.jpg"),
  new Character("Перси", "Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1114260770889412618/dFEj3lAkIsMWHP40CtxpS8SgEBdH4P3puK8D901u5WA.jpeg"),
];

function Character(title, member, image) {
  return {title, member, image};
}