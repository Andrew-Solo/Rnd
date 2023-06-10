import CardsGrid from "../../components/ui/cards/CardsGrid";
import DataContainer from "../../components/containers/data/DataContainer";

export default function CharactersPage () {
  return (
    <DataContainer title="Игры">
      <CardsGrid data={charactersData.map(character => ({...character, subtitle: character.member}))}/>
    </DataContainer>
  )
}

const charactersData = [
  new Character("dakurodo", "Дакуродо", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1114260412926533763/f5450d3932dab6af.jpg"),
  new Character("persi", "Перси","Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1114260770889412618/dFEj3lAkIsMWHP40CtxpS8SgEBdH4P3puK8D901u5WA.jpeg"),
];

function Character(name, title, member, image) {
  return {name, title, member, image};
}