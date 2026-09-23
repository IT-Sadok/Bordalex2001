export default interface CalendarProps {
    checkIn: Date | null;
    checkOut: Date | null;
    onCheckInChange: (date: Date) => void;
    onCheckOutChange: (date: Date | null) => void;
}