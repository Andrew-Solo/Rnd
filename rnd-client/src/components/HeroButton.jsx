import {Card, CardActionArea} from "@mui/material";
import {AddOutlined} from "@mui/icons-material";

export default function HeroButton() {
  return (
    <Card sx={{height: 1, minHeight: 200, minWidth: 200, borderRadius: "8px", background: "linear-gradient(96.34deg, #0FE9FF 0%, #19E7C1 51.56%, #0FFF8F 100%)"}}>
      <CardActionArea sx={{height: 1, display: "flex", justifyContent: "center", background: "linear-gradient(180deg, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.4) 100%)"}}>
        <AddOutlined sx={{width: 150, height: 150}}/>
      </CardActionArea>
    </Card>
  );
}