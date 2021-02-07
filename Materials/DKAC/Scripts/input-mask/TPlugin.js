(function ($) {
	$.fn.extend({
		decimalMask: function (maxlength) {
			return this.inputmask('decimal', {
				groupSeparator: ',',
				autoGroup: true,
				integerDigits: "*", //number of integerDigits
				digits: maxlength,
				digitsOptional: false,
				placeholder: '0',
				radixPoint: ".",
				groupSize: 3,
				autoGroup: false,
				allowPlus: true,
				allowMinus: true,
			});
		},
		soLenhSXMask: function () {
			return this.inputmask('999/99/AAA', {
				definitions: {
					'A': {
						validator: "[a-zA-Z]",
						casing: "upper"
					}
				}
			});
		}
	});
})(jQuery);