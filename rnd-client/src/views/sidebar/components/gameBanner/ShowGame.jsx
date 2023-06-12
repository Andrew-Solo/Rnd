import {Box, Button, Stack, Tooltip, Typography} from "@mui/material";
import Nickname from "../../../../components/ui/Nickname";

export default function ShowGame ({game}) {
  return (
    <Stack height={170} padding={1} sx={{backgroundImage: `url(${game.image})`, backgroundSize: "Cover", backgroundPosition: "center", backgroundRepeat: ""}}>
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
}