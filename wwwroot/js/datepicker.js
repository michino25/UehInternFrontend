function datePicker() {
    execute(() => {
        const selected_date_element = document.querySelector(
            ".date-picker .selected-date"
        );
        const dates_element = document.querySelector(".date-picker .dates");
        const mth_element = document.querySelector(
            ".date-picker .dates .month .mth"
        );
        const next_mth_element = document.querySelector(
            ".date-picker .dates .month .next-mth"
        );
        const prev_mth_element = document.querySelector(
            ".date-picker .dates .month .prev-mth"
        );
        const days_element = document.querySelector(
            ".date-picker .dates .days"
        );

        const months = [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
        ];

        let date = new Date();

        if (selected_date_element && selected_date_element.value) {
            let dateArr = selected_date_element.value.split("-");
            date = new Date(dateArr[2], dateArr[1] - 1, dateArr[0]);
        }

        let day = date.getDate();
        let month = date.getMonth();
        let year = date.getFullYear();

        if (mth_element) mth_element.textContent = months[month] + " " + year;

        // selected_date_element.value = formatDate(date, "-");
        // selected_date_element.dispatchEvent(new Event("change"));

        let selectedDate = date;
        let selectedDay = day;
        let selectedMonth = month;
        let selectedYear = year;

        populateDates();

        // EVENT LISTENERS
        days_element.addEventListener("click", toggleDatePicker);
        selected_date_element.addEventListener("click", toggleDatePicker);

        // Hide popup if we ain`t clicking on
        document.addEventListener("click", (event) => {
            if (
                !event.target.closest(".date-picker .dates .month") &&
                !event.target.closest(".date-picker .dates .days") &&
                !event.target.closest(".date-picker .selected-date")
            ) {
                dates_element.classList.add("hidden");
            }
        });

        next_mth_element.addEventListener("click", goToNextMonth);
        prev_mth_element.addEventListener("click", goToPrevMonth);

        // FUNCTIONS
        function toggleDatePicker() {
            dates_element.classList.toggle("hidden");
        }

        function goToNextMonth() {
            month++;
            if (month > 11) {
                month = 0;
                year++;
            }
            mth_element.textContent = months[month] + " " + year;
            populateDates();
        }

        function goToPrevMonth() {
            month--;
            if (month < 0) {
                month = 11;
                year--;
            }
            mth_element.textContent = months[month] + " " + year;
            populateDates();
        }

        function populateDates() {
            let amount_days;
            switch (month) {
                case 0: // January
                case 2: // March
                case 4: // May
                case 6: // July
                case 7: // August
                case 9: // October
                case 11: // December
                    amount_days = 31;
                    break;
                case 3: // April
                case 5: // June
                case 8: // September
                case 10: // November
                    amount_days = 30;
                    break;
                case 1: // February
                    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) {
                        amount_days = 29; // Leap year
                    } else {
                        amount_days = 28;
                    }
                    break;
            }

            days_element.innerHTML = "";

            // Determine the day of the week for the first day of the month
            const firstDayOfMonth = new Date(year, month, 1);
            const dayOfWeek = firstDayOfMonth.getDay();

            // Add empty elements for days before the first day of the month
            for (let i = 0; i < dayOfWeek; i++) {
                const empty_element = document.createElement("div");
                empty_element.classList.add(
                    "flex",
                    "justify-center",
                    "items-center",
                    "text-gray-800",
                    "rounded-lg",
                    "text-sm"
                );
                days_element.appendChild(empty_element);
            }

            for (let i = 0; i < amount_days; i++) {
                const day_element = document.createElement("div");
                day_element.classList.add(
                    "flex",
                    "justify-center",
                    "items-center",
                    "text-gray-800",
                    "rounded-lg",
                    "text-sm",
                    "hover:bg-gray-400",
                    "hover:bg-opacity-25"
                );
                day_element.textContent = i + 1;

                if (
                    selectedDay == i + 1 &&
                    selectedYear == year &&
                    selectedMonth == month
                ) {
                    day_element.classList.remove(
                        "hover:bg-gray-400",
                        "hover:bg-opacity-25"
                    );
                    day_element.classList.add("bg-teal-700", "text-white");
                }

                days_element.appendChild(day_element);

                day_element.addEventListener("click", function () {
                    selectedDate = new Date(
                        year + "-" + (month + 1) + "-" + (i + 1)
                    );
                    selectedDay = i + 1;
                    selectedMonth = month;
                    selectedYear = year;

                    selected_date_element.value = formatDate(selectedDate, "-");
                    selected_date_element.dispatchEvent(new Event("change"));

                    populateDates();
                });
            }
        }

        function formatDate(d, separate) {
            let day = d.getDate();
            if (day < 10) {
                day = "0" + day;
            }

            let month = d.getMonth() + 1;
            if (month < 10) {
                month = "0" + month;
            }

            let year = d.getFullYear();

            return day + separate + month + separate + year;
        }
    });
}

function execute(callback) {
    setTimeout(callback, 500);
}
