import ModuleHeader from "./ModuleHeader";
import {Box} from "@mui/material";
import UnitsGrid from "./cards/CardsGrid";
import {observer} from "mobx-react-lite";

const ModulePage = observer(({module}) => {
  return (
    <Box component="main" width={1} padding={4} gap={4} display="flex" flexDirection="column">
      <ModuleHeader title={module.title}/>
      <UnitsGrid tokens={[]}/>
    </Box>
  )
});

export default ModulePage

// const gamesData = [
//   new Game("mrak", "Мрак", "AndrewSolo", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971170581155900/c0a31dc92f19f998.png"),
//   new Game("lataif", "Сказки Латаифа", "Doktor", "https://cdn.discordapp.com/attachments/1104404469090881556/1113971208950648862/avatar.jpg"),
// ];
//
// function Game(name, title, owner, image) {
//   return {name, title, owner, image};
// }