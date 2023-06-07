import {SvgIcon} from "@mui/material";
import { ReactComponent as HomeSvg } from "../svg/home.svg";
import { ReactComponent as GroupSvg } from "../svg/group.svg";
import { ReactComponent as HistorySvg } from "../svg/history.svg";
import { ReactComponent as AddSvg } from "../svg/add.svg";
import { ReactComponent as FilterListSvg } from "../svg/filterList.svg";
import { ReactComponent as FilterListSvg400 } from "../svg/filterList400.svg";
import { ReactComponent as AccountCircleSvg } from "../svg/accountCircle.svg";
import { ReactComponent as LockSvg } from "../svg/lock.svg";
import { ReactComponent as MailSvg } from "../svg/mail.svg";

export function Home(props) {
  return (
    <SvgIcon {...props}>
      <HomeSvg/>
    </SvgIcon>
  );
}

export function Group(props) {
  return (
    <SvgIcon {...props}>
      <GroupSvg/>
    </SvgIcon>
  );
}

export function History(props) {
  return (
    <SvgIcon {...props}>
      <HistorySvg/>
    </SvgIcon>
  );
}

export function Add(props) {
  return (
    <SvgIcon {...props}>
      <AddSvg/>
    </SvgIcon>
  );
}

export function FilterList({weight = 300, ...props}) {

  const svg = weight === 400
    ? <FilterListSvg400/>
    : <FilterListSvg/>

  return (
    <SvgIcon {...props}>
      {svg}
    </SvgIcon>
  );
}

export function AccountCircle(props) {
  return (
    <SvgIcon {...props}>
      <AccountCircleSvg/>
    </SvgIcon>
  );
}

export function Lock(props) {
  return (
    <SvgIcon {...props}>
      <LockSvg/>
    </SvgIcon>
  );
}

export function Mail(props) {
  return (
    <SvgIcon {...props}>
      <MailSvg/>
    </SvgIcon>
  );
}
