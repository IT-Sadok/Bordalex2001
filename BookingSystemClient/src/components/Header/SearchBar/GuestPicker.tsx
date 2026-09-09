import { useRef, useState, type Dispatch, type SetStateAction } from "react";
import useClickOutside from "../../../hooks/useClickOutside";

const increment = (setter: Dispatch<SetStateAction<number>>) => {
  setter((prev) => prev + 1);
};

const decrement = (setter: Dispatch<SetStateAction<number>>) => {
  setter((prev) => (prev > 0 ? prev - 1 : 0));
};

export default function GuestPicker() {
  const [isOpen, setIsOpen] = useState(false);

  const [adults, setAdults] = useState(0);
  const [children, setChildren] = useState(0);
  const [infants, setInfants] = useState(0);
  const [pets, setPets] = useState(0);

  const guestPickerRef = useRef<HTMLDivElement>(null);

  useClickOutside(guestPickerRef, () => {
    setIsOpen(false);
  });

  const totalGuests = adults + children;

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

  return (
    <>
      <div ref={guestPickerRef} className="relative">
        <button
          type="button"
          onClick={() => setIsOpen(!isOpen)}
          className="rounded-2xl px-3 py-2 text-left hover:bg-gray-100"
        >
          <span className="block text-xs font-semibold">Who</span>
          <span className="block truncate text-sm text-gray-500">
            {guestSummary}
          </span>
        </button>

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
                    onClick={() => decrement(setAdults)}
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
                    onClick={() => increment(setAdults)}
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all cursor-pointer"
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
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all cursor-pointer"
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
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all cursor-pointer"
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
                    className="flex h-8 w-8 items-center justify-center rounded-full bg-gray-200 hover:bg-gray-300 transition-all cursor-pointer"
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
