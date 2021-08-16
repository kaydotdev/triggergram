import {
    Button,
    Badge
} from '@material-ui/core';
import {
    Notifications as NotificationsIcon
} from '@material-ui/icons';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        appbarButton: {
            color: '#fff'
        }
    }),
);


interface NotificationButtonProps {
    notificationCount: number;
}


function NotificationButton(props: NotificationButtonProps) {
    const classes = useStyles();

    return (
        <Button aria-label="notifications" className={classes.appbarButton}>
            <Badge color="secondary" badgeContent={props.notificationCount}>
                <NotificationsIcon />
            </Badge>
        </Button>
    );
}

export default NotificationButton;
