import { useRef, useState, type Dispatch, type SetStateAction } from "react";
import useClickOutside from "../../../../hooks/useClickOutside";
import type GuestPickerProps from "./GuestPickerProps";

const increment = (setter: Dispatch<SetStateAction<number>>) => {
  setter((prev) => prev + 1);
};

const decrement = (setter: Dispatch<SetStateAction<number>>) => {
  setter((prev) => (prev > 0 ? prev - 1 : 0));
};

const MAX_GUESTS = 16;
const MAX_INFANTS = 5;
const MAX_PETS = 5;

export default function GuestPicker({
  adults,
  children,
  infants,
  pets,
  setAdults,
  setChildren,
  setInfants,
  setPets,
  onAdultsChange,
  error,
}: GuestPickerProps) {
  const [isOpen, setIsOpen] = useState(false);

  const guestPickerRef = useRef<HTMLDivElement>(null);

  useClickOutside(guestPickerRef, () => {
    setIsOpen(false);
  });

  const totalGuests = adults + children;

  const canAddGuest = totalGuests < MAX_GUESTS;

  const guestSummaryParts = [];

  if (totalGuests > 0) {
    guestSummaryParts.push(
      `${totalGuests} ${totalGuests === 1 ? "guest" : "guests"}`,
    );
  }

  if (infants > 0) {
    guestSummaryParts.push(
      `${infants} ${infants === 1 ? "infant" : "infants"}`,
    );
  }

  if (pets > 0) {
    guestSummaryParts.push(`${pets} ${pets === 1 ? "pet" : "pets"}`);
  }

  const guestSummary =
    guestSummaryParts.length > 0 ? guestSummaryParts.join(", ") : "Add guests";

  const handleIncrementAdults = () => {
    const _adults = adults + 1;

    setAdults(_adults);
    onAdultsChange?.(_adults);
  }

  const handleDecrementAdults = () => {
    const _adults = adults > 0 ? adults - 1 : 0;

    setAdults(_adults);
    onAdultsChange?.(_adults);
  }

  return (
    <>
      <div ref={guestPickerRef} className="relative h-full min-w-0 flex-1">
        <button
          type="button"
          onClick={() => setIsOpen(!isOpen)}
          className={`flex h-full w-full min-w-0 flex-col justify-center rounded-2xl px-3 text-left hover:bg-gray-100 ${error ? "ring-2 ring-red-500" : ""}`}
        >
          <span className="block text-xs font-semibold">Who</span>
          <span className="block truncate text-sm text-gray-500">
            {guestSummary}
          </span>
        </button>

        {error && (
          <p className="absolute left-0 top-full mt-1 text-xs text-red-500">
            {error}
          </p>
        )}

        {isOpen && (
          <div className="absolute right-0 top-full z-10 mt-4 p-6 w-85 rounded-2xl bg-white shadow-lg">
            <div className="space-y-6">
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Adults</div>
                  <div className="text-sm text-gray-500">Ages 18 or above</div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    onClick={() => decrement(handleDecrementAdults)}
                    disabled={adults === 0}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${adults === 0 ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}`}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{adults}</span>
                  <button
                    type="button"
                    onClick={() => increment(handleIncrementAdults)}
                    disabled={!canAddGuest}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${!canAddGuest ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}`}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Children</div>
                  <div className="text-sm text-gray-500">Ages 2-17</div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    onClick={() => decrement(setChildren)}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${children === 0 ? "opacity-50 cursor-not-allowed" : "cursor-pointer"} `}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{children}</span>
                  <button
                    type="button"
                    onClick={() => increment(setChildren)}
                    disabled={!canAddGuest}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${!canAddGuest ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}`}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Infants</div>
                  <div className="text-sm text-gray-500">Under 2</div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    onClick={() => decrement(setInfants)}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${infants === 0 ? "opacity-50 cursor-not-allowed" : "cursor-pointer"} `}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{infants}</span>
                  <button
                    type="button"
                    onClick={() => increment(setInfants)}
                    disabled={infants >= MAX_INFANTS}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${infants >= MAX_INFANTS ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}`}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
              <div className="flex items-center justify-between">
                <div>
                  <div className="font-medium">Pets</div>
                  <div className="text-sm text-gray-500">
                    Bringing a service animal?
                  </div>
                </div>
                <div className="flex items-center gap-3">
                  <button
                    type="button"
                    onClick={() => decrement(setPets)}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${pets === 0 ? "opacity-50 cursor-not-allowed" : "cursor-pointer"} `}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                  </button>
                  <span>{pets}</span>
                  <button
                    type="button"
                    onClick={() => increment(setPets)}
                    disabled={pets >= MAX_PETS}
                    className={`flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all ${pets >= MAX_PETS ? "opacity-50 cursor-not-allowed" : "cursor-pointer"}`}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      className="size-4"
                    >
                      <path d="M8.75 3.75a.75.75 0 0 0-1.5 0v3.5h-3.5a.75.75 0 0 0 0 1.5h3.5v3.5a.75.75 0 0 0 1.5 0v-3.5h3.5a.75.75 0 0 0 0-1.5h-3.5v-3.5Z" />
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}
      </div>
    </>
  );
}
