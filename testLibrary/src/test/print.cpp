#include <stdio.h>

extern "C" {
void test_c() { fprintf(stderr, "test_c\n");
	}
}