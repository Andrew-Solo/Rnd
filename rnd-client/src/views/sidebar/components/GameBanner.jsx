import {Box, Button, Stack, Tooltip, Typography} from "@mui/material";
import {useStore} from "../../../stores/StoreProvider";
import {observer} from "mobx-react-lite";
import Nickname from "../../../components/ui/Nickname";

const GameBanner = observer(() => {
  const game = useStore().session.game;

  return (
    <Stack height={170} padding={1} sx={{backgroundImage: `url(${game.image})`, backgroundSize: "Cover", backgroundPosition: "center", backgroundRepeat: "no-repeat"}}>
      <Tooltip title="Открыть" placement="right" >
        <Button href={`/app/games/${game.name}`} color="neutral" sx={{width: 1, height: 1}}/></Tooltip>
      <Box display="flex" justifyContent="space-between" alignItems="flex-end">
        <Box color="neutral" sx={{flexDirection: "column", alignItems: "flex-start"}}>
          <Nickname name={game.owner.name}/>
          <Typography variant="body2">
            {game.title}
          </Typography>
        </Box>
        <Button>
          Сменить
        </Button>
      </Box>
    </Stack>
  );
});

export default GameBanner